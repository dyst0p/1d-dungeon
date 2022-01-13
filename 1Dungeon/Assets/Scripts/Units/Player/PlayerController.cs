using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

//todo: add interface IController, which will collect dependensies on Awake
public class PlayerController : BasePerformer
{
    [SerializeField] private float inputSensitivity;
    private float zInput;
    private float xInput;

    private PlayerData _player => _unit as PlayerData;
    private BaseMover _mover;

    private void Start()
    {
        _mover = transform.parent.GetComponentInChildren<BaseMover>();
    }

    private void Update()
    {
        if (!_mover.InMotion)
        {
            if (zInput > inputSensitivity)
                _mover.GoForward();
            if (zInput < -inputSensitivity)
                _mover.GoBackward();
        }

        if (!_mover.InRotation)
        {
            if (xInput > inputSensitivity)
                _mover.RotateClockwise();
            if (xInput < -inputSensitivity)
                _mover.RotateCounterclockwise();
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