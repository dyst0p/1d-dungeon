using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { There, LeftWall, Back, RightWall };

public class PlayerMovement : MonoBehaviour
{
    public bool inMotion = false;
    public bool isTransitionMade = false;
    public BaseCell motionTarget;
    public const float _transitionDistance = 2;
    [SerializeField] private float _distanceCovered;


    public PlayerData Player;
    public CellsManager Manager;
    public PlayerController Controller;

    public Direction CurrentMoveDirection = Direction.There;
    public Direction CurrentLookDirection = Direction.There;

    private void Update()
    {
        if (inMotion)
            CalculateNewPosition();
    }

    private void CalculateNewPosition() // need refactoring
    {
        Vector3 newPosition = new Vector3();
        _distanceCovered += Time.deltaTime * Player.WalkSpeed;

        if (_distanceCovered < _transitionDistance / 2)
        {
            float relativeTime = _distanceCovered / (_transitionDistance / 2);

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

            float relativeTime = (_transitionDistance - _distanceCovered) / (_transitionDistance / 2);

            if (CurrentMoveDirection == Direction.There)
                newPosition = motionTarget.GetPositionToBack(relativeTime);
            if (CurrentMoveDirection == Direction.Back)
                newPosition = motionTarget.GetPositionToThere(relativeTime);
        }
        if (_distanceCovered > _transitionDistance)
        {
            inMotion = false;
            isTransitionMade = false;
            newPosition = motionTarget.transform.position;
            motionTarget = null;
            _distanceCovered = 0;
        }

        Player.transform.position = newPosition;

        //JumpToCell(motionTarget);
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
}
