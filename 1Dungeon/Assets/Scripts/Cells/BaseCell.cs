using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCell : MonoBehaviour
{
    [SerializeField] private GameObject _unit;
    public GameObject Unit
    {
        get
        {
            return _unit;
        }
        set
        {
            _unit = value;
        }
    }
    [SerializeField] private int _index;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }

    [SerializeField] private BaseGuide _thereGuide;
    [SerializeField] private BaseGuide _backGuide;

    public Vector3 GetPositionToThere(float t) => _thereGuide.GetInterpolatedPosition(t);
    public Vector3 GetPositionToBack(float t) => _backGuide.GetInterpolatedPosition(t);
}
