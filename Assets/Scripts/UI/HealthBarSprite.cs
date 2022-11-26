using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HealthBarSprite : MonoBehaviour, IValueBar
{
    [SerializeField]
    Transform _fill;

    [SerializeField]
    int _fullValue;

    [ShowInInspector, ReadOnly]
    public float MaxValue { get; private set; }

    [ShowInInspector, ReadOnly]
    public float Value { get; private set; }

    public float Percent
    {
        get
        {
            if (MaxValue == 0) return 1;
            return Value / MaxValue;
        }
    }

    [Button]
    public void SetValue(float value)
    {
        Value = value;
        UpdateUI();
    }

    [Button]
    public void SetMaxValue(float value)
    {
        MaxValue = value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        SetPixels((int)((1 - Percent) * _fullValue));
    }

    private void SetPixels(int numFill)
    {
        _fill.localPosition = _fill.localPosition.With(x: (_fullValue - numFill) / 2.0f);
        _fill.localScale = _fill.localScale.With(x: numFill / 2.0f);
    }
}
