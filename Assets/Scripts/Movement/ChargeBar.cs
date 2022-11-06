using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LS.Utilities;
using Sirenix.OdinInspector;

public class ChargeBar : MonoBehaviour
{
    [SerializeField]
    ValueReference<float> _percent;

    [SerializeField]
    [Required]
    GameObject _chargeBarFull;

    [SerializeField]
    [Required]
    Transform _barTransform;
    
    void LateUpdate()
    {
        UpdateBar(_percent.Value);
    }

    void UpdateBar(float percent)
    {
        _barTransform.localScale = _barTransform.localScale.With(y: percent);
        _chargeBarFull.gameObject.SetActive(percent > 0);
    }
}
