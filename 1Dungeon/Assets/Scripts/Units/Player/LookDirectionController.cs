using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookDirectionController : BasePerformer
{
    private PlayerData _player => _unit as PlayerData;
    private Transform _playerView;
    [SerializeField] private readonly float _maxOffset = 1;

    private Vector2 _offset;

    public void ShiftFocalPoint(Vector2 shift)
    {
        _offset += shift;
    }

    private void Start()
    {
        _playerView = _player.view;
    }

    void Update()
    {
        _playerView.transform.LookAt(transform, Vector3.up);
    }

    private void LateUpdate()
    {
        transform.localPosition = new Vector3(_offset.x, 1 + _offset.y, 1);
    }
}