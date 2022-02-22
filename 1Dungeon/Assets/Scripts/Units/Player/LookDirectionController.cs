using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookDirectionController : BasePerformer
{
    //private PlayerData _player => _unit as PlayerData;
    private Transform _playerView;
    [SerializeField] private float _maxOffset = 60;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _minStickInput = 2;

    private Vector2 _offset = Vector2.zero;
    private Vector2 _lookStick = Vector2.zero;

    public void ShiftLookDirection(Vector2 shift)
    {
        if (shift.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        var scaledShift = shift * scaledRotateSpeed;
        _offset.x = Mathf.Clamp(_offset.x + scaledShift.x, -_maxOffset, _maxOffset);
        _offset.y = Mathf.Clamp(_offset.y - scaledShift.y, -_maxOffset, _maxOffset);
    }

    public void SetLookStick(Vector2 value)
    {
        if (value.sqrMagnitude < _minStickInput)
        {
            _lookStick = Vector2.zero;
            return;
        }
        _lookStick = value;
    }

    private void Start()
    {
        _playerView = _unit.view;
    }

    void Update()
    {
        ShiftLookDirection(_lookStick);
        var eulerAngles = new Vector3(_offset.y, _offset.x);
        var rotator = Quaternion.Euler(eulerAngles);
        _playerView.transform.rotation = _unit.transform.rotation * rotator;
    }
}