using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookDirectionController : MonoBehaviour
{
    [SerializeField] private PlayerData _player;
    [SerializeField] private Transform _playerView;
    [SerializeField] private readonly float _maxOffset = 1;

    [SerializeField] private Vector2 _offset;

    private Transform _playerTransform;

    public void ShiftFocalPoint(Vector2 shift)
    {
        _offset += shift;
    }

    private void Start()
    {
        _playerTransform = _player.transform;
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