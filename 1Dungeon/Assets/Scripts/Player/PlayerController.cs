using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// todo: add interface IController, which will collect dependensies on Awake
public class PlayerController : MonoBehaviour
{
    public float zInput;
    public float xInput;

    public PlayerData Player;
    public CellsManager Manager;
    public PlayerMovement Mover;

    [SerializeField] private float _inputSensitivity;

    private void Update()
    {
        if (!Mover.inMotion)
        {
            if (zInput > _inputSensitivity)
            {
                if (Mover.CurrentLookDirection == Direction.There)
                    Mover.GoThere();
                if (Mover.CurrentLookDirection == Direction.Back)
                    Mover.GoBack();
            }
            if (zInput < -_inputSensitivity)
            {
                if (Mover.CurrentLookDirection == Direction.Back)
                    Mover.GoThere();
                if (Mover.CurrentLookDirection == Direction.There)
                    Mover.GoBack();
            }
        }

        if (!Mover.inRotation)
        {
            if (xInput > _inputSensitivity)
                Mover.RotateClockwise();
            if (xInput < -_inputSensitivity)
                Mover.RotateÑounterclockwise();
        }
    }

    public void OnGoForward(InputAction.CallbackContext context)
    {
        zInput = context.ReadValue<float>();
    }

    public void OnRotateClockwise(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<float>();
    }
}
