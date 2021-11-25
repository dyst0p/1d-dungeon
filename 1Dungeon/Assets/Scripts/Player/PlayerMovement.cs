using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public MotionDirection CurrentDirection = MotionDirection.There;

    private void Update()
    {
        if (inMotion)
            CalculateNewPosition();
    }

    //public void OnMove(Vector2 input)
    //{
    //    if (!inMotion)
    //    {
    //        if (((input.x > _inputSensitivity) && (CurrentDirection == MotionDirection.There))
    //            || ((input.x < -_inputSensitivity) && (CurrentDirection == MotionDirection.Back)))
    //        {
    //            GoWhere();
    //        }
    //        if (((input.x > _inputSensitivity) && (CurrentDirection == MotionDirection.Back))
    //            || ((input.x < -_inputSensitivity) && (CurrentDirection == MotionDirection.There)))
    //        {
    //            GoBack();
    //        }
    //    }
    //}

    //public void OnWalkForward()
    //{
    //    if (inMotion)
    //        return;

    //    if (CurrentDirection == MotionDirection.There)
    //    {
    //        motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, 1));
    //        inMotion = true;
    //    }
    //    if (CurrentDirection == MotionDirection.Back)
    //    {
    //        motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, 1));
    //        inMotion = true;
    //    }
    //}

    //public void OnWalkBackward()
    //{
    //    if (inMotion)
    //        return;
    //    if (CurrentDirection == MotionDirection.There)
    //        JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, -1)));
    //    else
    //        JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, -1)));
    //}

    private void CalculateNewPosition() // need refactoring
    {
        //Vector3 newPosition = new Vector3();
        //_distanceCovered += Time.deltaTime * Player.WalkSpeed;

        //if (_distanceCovered < _transitionDistance / 2)
        //{
        //    float relativeTime = _distanceCovered / (_transitionDistance / 2);

        //    if (CurrentDirection == MotionDirection.There)
        //        newPosition = Player.CurrentCell.GetPositionToThere(relativeTime);
        //    if (CurrentDirection == MotionDirection.Back)
        //        newPosition = Player.CurrentCell.GetPositionToBack(relativeTime);
        //}
        //else
        //{
        //    isTransitionMade = true;
        //    Player.CurrentCell.Unit = null;
        //    Player.CurrentCell = motionTarget;

        //    float relativeTime = (_transitionDistance - _distanceCovered) / (_transitionDistance / 2);

        //    if (CurrentDirection == MotionDirection.There)
        //        newPosition = motionTarget.GetPositionToBack(relativeTime);
        //    if (CurrentDirection == MotionDirection.Back)
        //        newPosition = motionTarget.GetPositionToThere(relativeTime);
        //}
        //if (_distanceCovered > _transitionDistance)
        //{
        //    inMotion = false;
        //    isTransitionMade = false;
        //    newPosition = motionTarget.transform.position;
        //    motionTarget = null;
        //    _distanceCovered = 0;
        //}

        //Player.transform.position = newPosition;

        JumpToCell(motionTarget);
    }

    public void GoWhere()
    {
        motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, 1));
        inMotion = true;
    }

    public void GoBack()
    {
        motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, 1));
        inMotion = true;
    }

    private int GetTargetIndex(MotionDirection direction, int shift)
    {
        int currentIndex = Player.CurrentCell.Index;
        if ((direction != MotionDirection.There) && (direction != MotionDirection.Back))
            return currentIndex;

        int finalShift = (direction == MotionDirection.There) ? shift : -shift;
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
