using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

//todo: add interface IController, which will collect dependensies on Awake
public class PlayerController : MonoBehaviour
{
    public float zInput;
    public float xInput;

    public PlayerData player;
    public BaseMover mover;

    [SerializeField] private float inputSensitivity;

    private void Update()
    {
        if (!mover.InMotion)
        {
            if (zInput > inputSensitivity)
                mover.GoForward();
            if (zInput < -inputSensitivity)
                mover.GoBackward();
        }

        if (!mover.InRotation)
        {
            if (xInput > inputSensitivity)
                mover.RotateClockwise();
            if (xInput < -inputSensitivity)
                mover.RotateCounterclockwise();
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
