using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Default, There, LeftWall, Back, RightWall };

public class PlayerMovement : MonoBehaviour
{
    public bool inMotion = false;
    public bool inRotation = false;
    public bool isTransitionMade = false;

    public BaseCell motionTarget;
    public const float TransitionDistance = 2;
    [SerializeField] private float _distanceCovered;
    public Direction CurrentMoveDirection = Direction.There;

    public PlayerData Player;
    public CellsManager Manager;
    public PlayerController Controller;

    public Direction CurrentLookDirection = Direction.There;
    public Direction TargetLookDirection = Direction.There;
    public const float RightAngle = 90;
    [SerializeField] private float _angleRest = RightAngle;

    private void Update()
    {
        if (inMotion)
            CalculateNewPosition();
        if (inRotation)
            CalculateNewRotation();
    }

    private void CalculateNewPosition() // need refactoring
    {
        Vector3 newPosition = new Vector3();
        _distanceCovered += Time.deltaTime * Player.WalkSpeed;

        if (_distanceCovered < TransitionDistance / 2)
        {
            float relativeTime = _distanceCovered / (TransitionDistance / 2);

            if (CurrentMoveDirection == Direction.There)
                newPosition = Player.CurrentCell.GetPositionToThere(relativeTime);
            if (CurrentMoveDirection == Direction.Back)
                newPosition = Player.CurrentCell.GetPositionToBack(relativeTime);
        }
        else
        {
            isTransitionMade = true;
            Player.CurrentCell.Unit = null;
            Player.CurrentCell = motionTarget;

            float relativeTime = (TransitionDistance - _distanceCovered) / (TransitionDistance / 2);

            if (CurrentMoveDirection == Direction.There)
                newPosition = motionTarget.GetPositionToBack(relativeTime);
            if (CurrentMoveDirection == Direction.Back)
                newPosition = motionTarget.GetPositionToThere(relativeTime);
        }
        if (_distanceCovered > TransitionDistance)
        {
            inMotion = false;
            isTransitionMade = false;
            newPosition = motionTarget.transform.position;
            motionTarget = null;
            _distanceCovered = 0;
        }

        Player.transform.position = newPosition;
    }

    private void CalculateNewRotation()
    {
        float angleDelta = Time.deltaTime * Player.RotateSpeed;
        _angleRest -= angleDelta;

        if (_angleRest > 0)
        {
            float relativeTime = angleDelta / (_angleRest + angleDelta);

            Player.transform.forward = Vector3.Lerp(Player.transform.forward,
                GetForwardVectorByDirection(TargetLookDirection), relativeTime);
        }
        else
        {
            inRotation = false;
            _angleRest = RightAngle;
            CurrentLookDirection = TargetLookDirection;
            Player.transform.forward = GetForwardVectorByDirection(CurrentLookDirection);
        }
    }

    public void GoThere()
    {
        int motionIndex = GetTargetIndex(Direction.There, 1);
        if (motionIndex == Player.CurrentCell.Index)
            return;

        motionTarget = Manager.GetCellByIndex(motionIndex);
        inMotion = true;
        CurrentMoveDirection = Direction.There;

        Debug.Log("Go There");
    }

    public void GoBack()
    {
        int motionIndex = GetTargetIndex(Direction.Back, 1);
        if (motionIndex == Player.CurrentCell.Index)
            return;

        motionTarget = Manager.GetCellByIndex(motionIndex);
        inMotion = true;
        CurrentMoveDirection = Direction.Back;

        Debug.Log("Go Back");
    }

    private int GetTargetIndex(Direction direction, int shift)
    {
        int currentIndex = Player.CurrentCell.Index;
        if ((direction != Direction.There) && (direction != Direction.Back))
            return currentIndex;

        int finalShift = (direction == Direction.There) ? shift : -shift;
        int targetIndex = Mathf.Clamp(currentIndex + finalShift, 0, Manager.NumberOfCells - 1);
        return targetIndex;
    }

    private void JumpToCell(BaseCell targetCell)
    {
        Player.CurrentCell.Unit = null;
        targetCell.Unit = Player.gameObject;
        Player.transform.position = targetCell.transform.position;
        Player.CurrentCell = targetCell;
        inMotion = false;
    }

    public void RotateClockwise()
    {
        TargetLookDirection = GetRotatedDirection(CurrentLookDirection, true);
        inRotation = true;

        Debug.Log("Rotate Clockwise");
        Debug.Log(TargetLookDirection);
    }
    
    public void RotateÑounterclockwise()
    {
        TargetLookDirection = GetRotatedDirection(CurrentLookDirection, false);
        inRotation = true;

        Debug.Log("Rotate Counterclockwise");
        Debug.Log(TargetLookDirection);
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

    private Vector3 GetForwardVectorByDirection(Direction dir) => dir switch
    {
        Direction.There     => Vector3.forward,
        Direction.LeftWall  => Vector3.left,
        Direction.Back      => Vector3.back,
        Direction.RightWall => Vector3.right,
        _                   => Vector3.zero
    };
}
