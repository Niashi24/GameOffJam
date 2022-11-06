using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ChargeJumpBar : MonoBehaviour
{
    [SerializeField]
    [Required]
    WolfJump _wolfJump;

    [SerializeField]
    [Required]
    GameObject _chargeBarFull;

    [SerializeField]
    [Required]
    Transform _barTransform;
    
    void OnEnable()
    {
        _wolfJump.OnStartChargeJump += EnableBar;
        _wolfJump.OnExecuteChargeJump += OnFinishCharge;
    }

    void OnDisable()
    {
        _wolfJump.OnStartChargeJump -= EnableBar;
        _wolfJump.OnExecuteChargeJump -= OnFinishCharge;
    }

    void EnableBar()
    {
        _chargeBarFull.SetActive(true);
    }

    void DisableBar()
    {
        _chargeBarFull.SetActive(false);
    }

    void OnFinishCharge(float percent, bool successful)
    {
        DisableBar();
    }

    void LateUpdate() 
    {
        _barTransform.localScale = _barTransform.localScale.With(y: _wolfJump.PercentCharge);    
    }
}
