using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LinearGuide : BaseGuide
{
    [FormerlySerializedAs("StartTransform")] [SerializeField] Transform startTransform;
    [FormerlySerializedAs("EndTransform")] [SerializeField] Transform endTransform;

    private void Start()
    {
        StartPoint = startTransform.position;
        EndPoint = endTransform.position;
    }

    public override Vector3 GetInterpolatedPosition(float t)
    {
        return Vector3.Lerp(StartPoint, EndPoint, t);
    }
}
