using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePerformer : MonoBehaviour
{
    protected BaseUnitData _unit;

    protected void OnValidate()
    {
        _unit = GetComponentInParent<BaseUnitData>();
    }
}
