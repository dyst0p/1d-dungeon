using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGuide : MonoBehaviour
{
    protected Vector3 StartPoint;
    protected Vector3 EndPoint;

    public virtual Vector3 GetInterpolatedPosition(float t)
    {
        return Vector3.zero;
    }
}
