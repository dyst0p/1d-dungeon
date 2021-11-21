using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearGuide : BaseGuide
{
    [SerializeField] Transform StartTransform;
    [SerializeField] Transform EndTransform;

    private void Start()
    {
        StartPoint = StartTransform.position;
        EndPoint = EndTransform.position;
    }

    public override Vector3 GetInterpolatedPosition(float t)
    {
        return Vector3.Lerp(StartPoint, EndPoint, t);
    }
}
