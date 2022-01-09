using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: added interface IData
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

    [SerializeField] private float _walkSpeed; // 4 is pretty good
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
    
    [SerializeField] private float _rotateSpeed; // 720 is good for quick turn-around
    public float RotateSpeed
    {
        get
        {
            return _rotateSpeed;
        }
        set
        {
            _rotateSpeed = value;
        }
    }
}
