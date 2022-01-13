using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMover : BasePerformer
{
    protected const float TransitionDistance = 2;
    protected const float RightAngle = 90;

    protected BaseCell motionTarget;

    protected bool _isTransitionMade = false; // for canceling move
    protected float distanceCovered;
    protected float angleRest = RightAngle;

    protected Direction currentMoveDirection = Direction.There;
    public Direction CurrentLookDirection = Direction.There;
    protected Direction TargetLookDirection = Direction.There;
    
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