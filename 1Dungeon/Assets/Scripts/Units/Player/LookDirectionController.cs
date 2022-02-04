using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookDirectionController : BasePerformer
{
    private PlayerData _player => _unit as PlayerData;
    private Transform _playerView;
    [SerializeField] private float _maxOffset = 1;
    [SerializeField] private float _rotateSpeed;

    private Vector2 _offset;

    public void ShiftFocalPoint(Vector2 shift)
    {
        if (shift.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        var scaledShift = shift * scaledRotateSpeed;
        _offset += scaledShift;
        if (_offset.magnitude > _maxOffset)
            _offset = _offset.normalized * _maxOffset;
        //_offset = shift;
    }

    private void Start()
    {
        _playerView = _player.view;
    }

    void Update()
    {
        transform.localPosition = new Vector3(_offset.x, 1 + _offset.y, 1);
        _playerView.transform.LookAt(transform, Vector3.up);
    }
}