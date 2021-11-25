using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool inMotion = false;
    public bool isTransitionMade = false;
    public BaseCell motionTarget;
    public const float _transitionDistance = 2;
    [SerializeField] private float _distanceCovered;


    public PlayerData Player;
    public CellsManager Manager;
    public PlayerMovement Mover;

    public Direction CurrentDirection = Direction.There;

    [SerializeField] private float _inputSensitivity;

    //private void Update()
    //{
    //    if (inMotion)
    //        CalculateNewPosition();
    //}

    public void OnGoForward()
    {
        if (!Mover.inMotion)
        {
            if (Mover.CurrentLookDirection == Direction.There)
                Mover.GoThere();
            if (Mover.CurrentLookDirection == Direction.Back)
                Mover.GoBack();
        }
    }

    public void OnGoBackward()
    {
        if (!Mover.inMotion)
        {
            if (Mover.CurrentLookDirection == Direction.There)
                Mover.GoBack();
            if (Mover.CurrentLookDirection == Direction.Back)
                Mover.GoThere();
        }
    }

    //public void OnMove(Vector2 input)
    //{
    //    if (!inMotion)
    //    {
    //        if (((input.x > _inputSensitivity) && (CurrentDirection == Direction.There))
    //            || ((input.x < -_inputSensitivity) && (CurrentDirection == Direction.Back)))
    //        {
    //            GoWhere();
    //        }
    //        if (((input.x > _inputSensitivity) && (CurrentDirection == Direction.Back))
    //            || ((input.x < -_inputSensitivity) && (CurrentDirection == Direction.There)))
    //        {
    //            GoBack();
    //        }
    //    }
    //}

    //public void OnGoForward()
    //{
    //    if (Mover.inMotion)
    //        return;

    //    if (Mover.CurrentDirection == Direction.There)
    //    {
    //        motionTarget = Manager.GetCellByIndex(GetTargetIndex(Direction.There, 1));
    //        inMotion = true;
    //    }
    //    if (CurrentDirection == Direction.Back)
    //    {
    //        motionTarget = Manager.GetCellByIndex(GetTargetIndex(Direction.Back, 1));
    //        inMotion = true;
    //    }
    //}

    //public void OnGoBackward()
    //{
    //    if (inMotion)
    //        return;
    //    if (CurrentDirection == Direction.There)
    //        JumpToCell(Manager.GetCellByIndex(GetTargetIndex(Direction.There, -1)));
    //    else
    //        JumpToCell(Manager.GetCellByIndex(GetTargetIndex(Direction.Back, -1)));
    //}

    //private void CalculateNewPosition() // need refactoring
    //{
    //    Vector3 newPosition = new Vector3();
    //    _distanceCovered += Time.deltaTime * Player.WalkSpeed;

    //    if (_distanceCovered < _transitionDistance / 2)
    //    {
    //        float relativeTime = _distanceCovered / (_transitionDistance / 2);

    //        if (CurrentDirection == Direction.There)
    //            newPosition = Player.CurrentCell.GetPositionToThere(relativeTime);
    //        if (CurrentDirection == Direction.Back)
    //            newPosition = Player.CurrentCell.GetPositionToBack(relativeTime);
    //    }
    //    else
    //    {
    //        isTransitionMade = true;
    //        Player.CurrentCell.Unit = null;
    //        Player.CurrentCell = motionTarget;

    //        float relativeTime = (_transitionDistance - _distanceCovered) / (_transitionDistance / 2);

    //        if (CurrentDirection == Direction.There)
    //            newPosition = motionTarget.GetPositionToBack(relativeTime);
    //        if (CurrentDirection == Direction.Back)
    //            newPosition = motionTarget.GetPositionToThere(relativeTime);
    //    }
    //    if (_distanceCovered > _transitionDistance)
    //    {
    //        inMotion = false;
    //        isTransitionMade = false;
    //        newPosition = motionTarget.transform.position;
    //        motionTarget = null;
    //        _distanceCovered = 0;
    //    }

    //    Player.transform.position = newPosition;
    //}

    //private void GoWhere()
    //{
    //    motionTarget = Manager.GetCellByIndex(GetTargetIndex(Direction.There, 1));
    //    inMotion = true;
    //}

    //private void GoBack()
    //{
    //    motionTarget = Manager.GetCellByIndex(GetTargetIndex(Direction.Back, 1));
    //    inMotion = true;
    //}

    //private int GetTargetIndex(Direction direction, int shift)
    //{
    //    Debug.Log(Player);
    //    Debug.Log(Player.CurrentCell);
    //    Debug.Log(Player.CurrentCell.Index);
    //    int currentIndex = Player.CurrentCell.Index;
    //    int finalShift = (direction == Direction.There) ? shift : -shift;
    //    int targetIndex = Mathf.Clamp(currentIndex + finalShift, 0, Manager.NumberOfCells - 1);
    //    return targetIndex;
    //}

    //private void JumpToCell(BaseCell targetCell)
    //{
    //    Player.CurrentCell.Unit = null;
    //    targetCell.Unit = Player.gameObject;
    //    Player.transform.position = targetCell.transform.position;
    //    Player.CurrentCell = targetCell;
    //}
}
