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


}
