using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using UnityEngine;

[System.Serializable]
public class ValueBarObjectReference : IValueBar
{
    [SerializeField]
    ObjectReference<IValueBar> _valueBar;

    public float MaxValue => _valueBar.Value.MaxValue;

    public float Value => _valueBar.Value.Value;

    public float Percent => _valueBar.Value.Percent;

    public void SetMaxValue(float value) => _valueBar.Value.SetMaxValue(value);

    public void SetValue(float value) => _valueBar.Value.SetValue(value);
}
