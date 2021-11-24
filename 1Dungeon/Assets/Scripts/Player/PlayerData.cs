using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private BaseCell _currentCell;
    public BaseCell CurrentCell
    {
        get
        {
            return _currentCell;
        }
        set
        {
            _currentCell = value;
        }
    }

    [SerializeField] private float _walkSpeed;
    public float WalkSpeed
    {
        get
        {
            return _walkSpeed;
        }
        set
        {
            _walkSpeed = value;
        }
    }
}
