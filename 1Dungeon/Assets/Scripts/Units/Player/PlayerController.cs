using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : BasePerformer
{
    [SerializeField] private float _inputSensitivity;
    [SerializeField] private float _lookMouseSpeed;
    [SerializeField] private float _lookGamepadSpeed;
    private float _zInput;
    private float _xInput;

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
            if (_zInput > _inputSensitivity)
                _mover.GoForward();
            if (_zInput < -_inputSensitivity)
                _mover.GoBackward();
        }

        if (!_mover.InRotation)
        {
            if (_xInput > _inputSensitivity)
                _mover.RotateClockwise();
            if (_xInput < -_inputSensitivity)
                _mover.RotateCounterclockwise();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context) =>
        _zInput = context.ReadValue<float>();

    public void OnRotateClockwise(InputAction.CallbackContext context) =>
        _xInput = context.ReadValue<float>();

    public void OnLookMouseInput(InputAction.CallbackContext context) =>
        _lookController.ShiftLookDirection(context.ReadValue<Vector2>() * _lookMouseSpeed);
    
    public void OnLookGamepadInput(InputAction.CallbackContext context) =>
        _lookController.SetLookStick(context.ReadValue<Vector2>() * _lookGamepadSpeed);
}