using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : BasePerformer
{
    [SerializeField] private float inputSensitivity;
    private float zInput;
    private float xInput;

    private PlayerData _player => _unit as PlayerData;
    private BaseMover _mover;
    private LookDirectionController _lookController;

    private void Start()
    {
        _mover = transform.parent.GetComponentInChildren<BaseMover>();
        _lookController = transform.parent.GetComponentInChildren<LookDirectionController>();
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
    
    public void OnLookInput(InputAction.CallbackContext context)
    {
        var lookInput = context.ReadValue<Vector2>();
        var shift = lookInput.magnitude > 1 ? lookInput.normalized : lookInput;
        _lookController.ShiftFocalPoint(shift);
    }
}