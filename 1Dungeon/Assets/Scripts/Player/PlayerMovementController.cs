using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MotionDirection { There, LeftWall, Back, RightWall };

public class PlayerMovementController : MonoBehaviour
{
    public float speed = 1f; // need to relocate to PlayerData
    public bool inMotion = false;
    public bool isTransitionMade = false;
    public BaseCell motionTarget;
    [SerializeField] private float _transitTime;
    [SerializeField] private float _timeInMotion;


    public PlayerData Player;
    public CellsManager Manager;

    public MotionDirection CurrentDirection = MotionDirection.There;

    private void Update()
    {
        if (inMotion)
        {
            Vector3 newPosition = new Vector3();
            _timeInMotion += Time.deltaTime;

            if (_timeInMotion < _transitTime / 2)
            {
                float relativeTime = _timeInMotion / (_transitTime / 2);

                if (CurrentDirection == MotionDirection.There)
                    newPosition = Player.CurrentCell.GetPositionToThere(relativeTime);
                if (CurrentDirection == MotionDirection.Back)
                    newPosition = Player.CurrentCell.GetPositionToBack(relativeTime);
            }
            else
            {
                isTransitionMade = true;
                Player.CurrentCell.Unit = null;
                Player.CurrentCell = motionTarget;

                float relativeTime = (_transitTime - _timeInMotion) / (_transitTime / 2);

                if (CurrentDirection == MotionDirection.There)
                    newPosition = motionTarget.GetPositionToBack(relativeTime);
                if (CurrentDirection == MotionDirection.Back)
                    newPosition = motionTarget.GetPositionToThere(relativeTime);
            }
            if (_timeInMotion > _transitTime)
            {
                inMotion = false;
                isTransitionMade = false;
                newPosition = motionTarget.transform.position;
                motionTarget = null;
                _timeInMotion = 0;
            }

            Player.transform.position = newPosition;
        }
    }

    public void OnWalkForward()
    {
        if (inMotion)
            return;

        _transitTime = 4 / speed;

        if (CurrentDirection == MotionDirection.There)
        {
            motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, 1));
            inMotion = true;
        }
        if (CurrentDirection == MotionDirection.Back)
        {
            motionTarget = Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, 1));
            inMotion = true;
        }
    }

    public void OnWalkBackward()
    {
        if (inMotion)
            return;
        if (CurrentDirection == MotionDirection.There)
            JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, -1)));
        else
            JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, -1)));
    }

    public void OnFire()
    {
        Debug.Log("Fire Message");
    }

    private int GetTargetIndex(MotionDirection direction, int shift)
    {
        Debug.Log(Player);
        Debug.Log(Player.CurrentCell);
        Debug.Log(Player.CurrentCell.Index);
        int currentIndex = Player.CurrentCell.Index;
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
    }
}
