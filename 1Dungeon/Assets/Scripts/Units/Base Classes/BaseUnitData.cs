using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitData : MonoBehaviour
{
    public BaseCell currentCell;
    
    public Transform view;
    
    public float walkSpeed; // 4 is pretty good

    public float rotateSpeed; // 720 is good for quick turn-around
}
