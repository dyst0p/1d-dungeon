using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// todo: added interface IData
public class PlayerData : MonoBehaviour
{
    [SerializeField] private BaseCell currentCell;
    public BaseCell CurrentCell
    {
        get
        {
            return currentCell;
        }
        set
        {
            currentCell = value;
        }
    }

    [SerializeField] private float walkSpeed; // 4 is pretty good
    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }
        set
        {
            walkSpeed = value;
        }
    }
    
    [SerializeField] private float rotateSpeed; // 720 is good for quick turn-around
    public float RotateSpeed
    {
        get
        {
            return rotateSpeed;
        }
        set
        {
            rotateSpeed = value;
        }
    }
}
