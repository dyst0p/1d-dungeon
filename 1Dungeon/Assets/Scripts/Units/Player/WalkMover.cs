using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Direction { Default, There, LeftWall, Back, RightWall };

public class WalkMover : BaseMover
{

    protected override void CalculateNewPosition() // todo: refactoring
    {
        Vector3 newPosition = new Vector3();
        distanceCovered += Time.deltaTime * _unit.walkSpeed;

        if (distanceCovered < TransitionDistance / 2)
        {
            float relativeTime = distanceCovered / (TransitionDistance / 2);

            if (currentMoveDirection == Direction.There)
                newPosition = _unit.currentCell.GetPositionToThere(relativeTime);
            if (currentMoveDirection == Direction.Back)
                newPosition = _unit.currentCell.GetPositionToBack(relativeTime);
        }
        else
        {
            _isTransitionMade = true;
            _unit.currentCell.Unit = null;
            _unit.currentCell = motionTarget;
            _unit.currentCell.Unit = _unit.transform.gameObject;

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
        }

        _unit.transform.position = newPosition;
    }

    protected override void CalculateNewRotation()
    {
        float angleDelta = Time.deltaTime * _unit.rotateSpeed;
        angleRest -= angleDelta;

        if (angleRest > 0)
        {
            float relativeTime = angleDelta / (angleRest + angleDelta);

            _unit.transform.forward = Vector3.Lerp(_unit.transform.forward,
                GetForwardVectorByDirection(TargetLookDirection), relativeTime);
        }
        else
        {
            InRotation = false;
            angleRest = RightAngle;
            CurrentLookDirection = TargetLookDirection;
            _unit.transform.forward = GetForwardVectorByDirection(CurrentLookDirection);
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
        if (motionIndex == _unit.currentCell.Index)
            return;
        
        var target = CellsManager.GetCellByIndex(motionIndex);

        if (target.Unit != null)
            return;
        
        motionTarget = target;
        InMotion = true;
        currentMoveDirection = Direction.There;
    }

    public void GoBack()
    {
        int motionIndex = GetTargetIndex(Direction.Back, 1);
        if (motionIndex == _unit.currentCell.Index)
            return;

        motionTarget = CellsManager.GetCellByIndex(motionIndex);
        InMotion = true;
        currentMoveDirection = Direction.Back;
    }

    private int GetTargetIndex(Direction direction, int shift)
    {
        int currentIndex = _unit.currentCell.Index;
        if ((direction != Direction.There) && (direction != Direction.Back))
            return currentIndex;

        int finalShift = (direction == Direction.There) ? shift : -shift;
        int targetIndex = Mathf.Clamp(currentIndex + finalShift, 0, CellsManager.NumberOfCells - 1);
        return targetIndex;
    }

    private void JumpToCell(BaseCell targetCell)
    {
        _unit.currentCell.Unit = null;
        targetCell.Unit = _unit.transform.gameObject;
        _unit.transform.position = targetCell.transform.position;
        _unit.currentCell = targetCell;
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
