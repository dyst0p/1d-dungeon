using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    public bool inMotion = false;
    public BaseCell motionTarget;
    [SerializeField] private float _transitTime = 3;


    public PlayerData Player;
    public CellsManager Manager;

    public enum MotionDirection { There, Back };
    public MotionDirection CurrentDirection = MotionDirection.There;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWalkForward()
    {
        if (inMotion)
            return;
        if (CurrentDirection == MotionDirection.There)
            JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.There, 1)));
        else
            JumpToCell(Manager.GetCellByIndex(GetTargetIndex(MotionDirection.Back, 1)));
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
        Debug.Log("Got Message");
    }

    private int GetTargetIndex(MotionDirection direction, int shift)
    {
        int currentIndex = Player.CurrentCell.Index;
        int finalShift = (direction == MotionDirection.There) ? shift : -shift;
        return currentIndex + finalShift; // need to add clamp to prevent index out of the bounds
    }

    private void JumpToCell(BaseCell targetCell)
    {
        Player.CurrentCell.Unit = null;
        targetCell.Unit = Player.gameObject;
        Player.transform.position = targetCell.transform.position;
        Player.CurrentCell = targetCell;
    }
}
