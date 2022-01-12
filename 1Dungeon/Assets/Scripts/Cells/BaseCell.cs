using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseCell : MonoBehaviour
{
    [FormerlySerializedAs("_unit")] [SerializeField] private GameObject unit;
    public GameObject Unit
    {
        get
        {
            return unit;
        }
        set
        {
            unit = value;
        }
    }
    [FormerlySerializedAs("_index")] [SerializeField] private int index;
    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    [FormerlySerializedAs("_thereGuide")] [SerializeField] private BaseGuide thereGuide;
    [FormerlySerializedAs("_backGuide")] [SerializeField] private BaseGuide backGuide;

    public Vector3 GetPositionToThere(float t) => thereGuide.GetInterpolatedPosition(t);
    public Vector3 GetPositionToBack(float t) => backGuide.GetInterpolatedPosition(t);
}
