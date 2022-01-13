using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Direction { Default, There, LeftWall, Back, RightWall };

public class WalkMover : BaseMover
{
    // todo: clean up fields
    private bool _isTransitionMade = false;

    public BaseCell motionTarget;
    public const float TransitionDistance = 2;
    [SerializeField] private float distanceCovered;
    public Direction currentMoveDirection = Direction.There;

    public PlayerData player;

    public Direction CurrentLookDirection = Direction.There;
    public Direction TargetLookDirection = Direction.There;
    public const float RightAngle = 90;
    [SerializeField] private float angleRest = RightAngle;

    protected override void CalculateNewPosition() // need refactoring
    {
        Vector3 newPosition = new Vector3();
        distanceCovered += Time.deltaTime * player.WalkSpeed;

        if (distanceCovered < TransitionDistance / 2)
        {
            float relativeTime = distanceCovered / (TransitionDistance / 2);

            if (currentMoveDirection == Direction.There)
                newPosition = player.CurrentCell.GetPositionToThere(relativeTime);
            if (currentMoveDirection == Direction.Back)
                newPosition = player.CurrentCell.GetPositionToBack(relativeTime);
        }
        else
        {
            _isTransitionMade = true;
            player.CurrentCell.Unit = null;
            player.CurrentCell = motionTarget;
            player.CurrentCell.Unit = player.gameObject;

            float relativeTime = (TransitionDistance - distanceCovered) / (TransitionDistance / 2);

            if (currentMoveDirection == Direction.There)
                newPosition = motionTarget.GetPositionToBack(relativeTime);
            if (currentMoveDirection == Direction.Back)
                newPosition = motionTarget.GetPositionToThere(relativeTime);
        }
        if (distanceCovered > TransitionDistance)
        {
            InMotion = false;
            _isTransitionMade = false;
            newPosition = motionTarget.transform.position;
            motionTarget = null;
            distanceCovered = 0;
            // todo: add unit to cell's Unit field
        }

        player.transform.position = newPosition;
    }

    protected override void CalculateNewRotation()
    {
        float angleDelta = Time.deltaTime * player.RotateSpeed;
        angleRest -= angleDelta;

        if (angleRest > 0)
        {
            float relativeTime = angleDelta / (angleRest + angleDelta);

            player.transform.forward = Vector3.Lerp(player.transform.forward,
                GetForwardVectorByDirection(TargetLookDirection), relativeTime);
        }
        else
        {
            InRotation = false;
            angleRest = RightAngle;
            CurrentLookDirection = TargetLookDirection;
            player.transform.forward = GetForwardVectorByDirection(CurrentLookDirection);
        }
    }

    public override void GoForward()
    {
        if (CurrentLookDirection == Direction.There)
            GoThere();
        if (CurrentLookDirection == Direction.Back)
            GoBack();
    }

    public override void GoBackward()
    {
        if (CurrentLookDirection == Direction.Back)
            GoThere();
        if (CurrentLookDirection == Direction.There)
            GoBack();
    }
    
    public void GoThere()
    {
        int motionIndex = GetTargetIndex(Direction.There, 1);
        if (motionIndex == player.CurrentCell.Index)
            return;

        motionTarget = CellsManager.GetCellByIndex(motionIndex);
        InMotion = true;
        currentMoveDirection = Direction.There;
    }

    public void GoBack()
    {
        int motionIndex = GetTargetIndex(Direction.Back, 1);
        if (motionIndex == player.CurrentCell.Index)
            return;

        motionTarget = CellsManager.GetCellByIndex(motionIndex);
        InMotion = true;
        currentMoveDirection = Direction.Back;
    }

    private int GetTargetIndex(Direction direction, int shift)
    {
        int currentIndex = player.CurrentCell.Index;
        if ((direction != Direction.There) && (direction != Direction.Back))
            return currentIndex;

        int finalShift = (direction == Direction.There) ? shift : -shift;
        int targetIndex = Mathf.Clamp(currentIndex + finalShift, 0, CellsManager.NumberOfCells - 1);
        return targetIndex;
    }

    private void JumpToCell(BaseCell targetCell)
    {
        player.CurrentCell.Unit = null;
        targetCell.Unit = player.gameObject;
        player.transform.position = targetCell.transform.position;
        player.CurrentCell = targetCell;
        InMotion = false;
    }

    public override void RotateClockwise()
    {
        TargetLookDirection = GetRotatedDirection(CurrentLookDirection, true);
        InRotation = true;
    }
    
    public override void RotateCounterclockwise()
    {
        TargetLookDirection = GetRotatedDirection(CurrentLookDirection, false);
        InRotation = true;
    }

    private Direction GetRotatedDirection(Direction currentDirection, bool clockwise)
    {
        if (clockwise)
        {
            return currentDirection switch
            {
                Direction.There     => Direction.RightWall,
                Direction.RightWall => Direction.Back,
                Direction.Back      => Direction.LeftWall,
                Direction.LeftWall  => Direction.There,
                _                   => Direction.Default
            };
        }
        else
        {
            return currentDirection switch
            {
                Direction.There     => Direction.LeftWall,
                Direction.LeftWall  => Direction.Back,
                Direction.Back      => Direction.RightWall,
                Direction.RightWall => Direction.There,
                _                   => Direction.Default
            };
        }
    }
    
    // todo: add condition for turned cells
    private Vector3 GetForwardVectorByDirection(Direction dir) => dir switch
    {
        Direction.There     => Vector3.forward,
        Direction.LeftWall  => Vector3.left,
        Direction.Back      => Vector3.back,
        Direction.RightWall => Vector3.right,
        _                   => Vector3.zero
    };
}
