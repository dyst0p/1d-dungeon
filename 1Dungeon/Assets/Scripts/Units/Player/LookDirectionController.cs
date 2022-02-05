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

    private Vector2 _offset = Vector2.zero;

    public void ShiftLookDirection(Vector2 shift)
    {
        if (shift.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        var scaledShift = shift * scaledRotateSpeed;
        _offset.x = Mathf.Clamp(_offset.x + scaledShift.x, -_maxOffset, _maxOffset);
        _offset.y = Mathf.Clamp(_offset.y - scaledShift.y, -_maxOffset, _maxOffset);
        // if (_offset.magnitude > _maxOffset)
        //     _offset = _offset.normalized * _maxOffset;
        //_offset = shift;
    }

    private void Start()
    {
        _playerView = _unit.view;
    }

    void Update()
    {
        //transform.localPosition = new Vector3(_offset.x, 1 + _offset.y, 1);
        var eulerAngles = new Vector3(_offset.y, _offset.x);
        var rotator = Quaternion.Euler(eulerAngles);
        //_playerView.transform.LookAt(transform, Vector3.up);
        _playerView.transform.rotation = _unit.transform.rotation * rotator;
    }
}