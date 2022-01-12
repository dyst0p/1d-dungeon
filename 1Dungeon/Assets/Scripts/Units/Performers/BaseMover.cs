using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMover : MonoBehaviour
{
    public bool InMotion { get; protected set; }
    public bool InRotation { get; protected set; }

    private void Update()
    {
        if (InMotion)
            CalculateNewPosition();
        if (InRotation)
            CalculateNewRotation();
    }

    public abstract void GoForward();

    public abstract void GoBackward();

    public abstract void RotateClockwise();

    public abstract void RotateCounterclockwise();

    protected abstract void CalculateNewPosition();

    protected abstract void CalculateNewRotation();
}