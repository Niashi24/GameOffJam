using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour, IValueBar
{
    [SerializeField]
    Image _image;

    [SerializeField]
    float _defaultValue;

    [ShowInInspector, ReadOnly]
    public float MaxValue {get; private set;}

    [ShowInInspector, ReadOnly]
    public float Value {get; private set;}

    public float Percent
    {
        get
        {
            if (MaxValue == 0) return 1;
            return Value/MaxValue;
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
        _image.rectTransform.sizeDelta = _image.rectTransform.sizeDelta.With(x:Mathf.Floor((1-Percent) * _defaultValue));
    }

    #if UNITY_EDITOR
    [Button]
    void SetDefaultValue(Image _background, int borderSize = 1)
    {
        if (_background is null) return;

        _defaultValue = _background.rectTransform.sizeDelta.x - borderSize * 2;
    }
    #endif
}