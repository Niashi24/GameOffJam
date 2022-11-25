using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
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

    void Start()
    {

    }

    [Button]
    void SetDefaultValue()
    {
        _defaultValue = _image.rectTransform.sizeDelta.x;
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
}