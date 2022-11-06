using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class WolfJump : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    ValueReference<bool> _isGrounded;

    [SerializeField]
    [Required]
    Camera _mainCamera;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    ObjectValueReference<ICoyoteTime> _coyoteTimeHandler;

    [Header("Parameters")]
    [SerializeField]
    float _maxJumpVelocity = 150f;

    [SerializeField]
    AnimationCurve _jumpVelocityCurve;

    [SerializeField]
    float _timeToFullCharge = 0.5f;

    [ShowInInspector, ReadOnly]
    float _chargeTimer = 0;

    [ShowInInspector, ReadOnly]
    bool _charging = false;

    //triggered when starting to charge a jump
    public Action OnStartChargeJump;
    //triggered when the jump is fully charged
    //the bool represents whether it was a successful jump or not
    public Action OnFullCharge;
    //triggered when executing the jump
    //float is percentage of full (0-1)
    //bool is whether the jump was successful
    public Action<float, bool> OnExecuteChargeJump;

    public float PercentCharge 
    {
        get 
        {
            if (_timeToFullCharge == 0)
                return 1;
            return _chargeTimer / _timeToFullCharge;
        }
    }

    void FixedUpdate()
    {
        if (!_charging && _input.Space && _isGrounded.Value)
        {
            _charging = true;
            OnStartChargeJump?.Invoke();
            //remember to check if grounded when finishing
        }
        else if (_charging)
        {
            if (_input.Space)
            {
                if (_chargeTimer < 1)
                {
                    if (_timeToFullCharge == 0) _chargeTimer = 0;
                    else _chargeTimer = Mathf.Min(_timeToFullCharge, _chargeTimer + Time.deltaTime);

                    if (PercentCharge == 1)
                        OnFullCharge?.Invoke();
                }
            }
            else
            {

                if (_isGrounded.Value)
                {
                    _rbdy2D.velocity += _mainCamera.GetUnitVectorFromPositionToMouse(transform.position) * GetAdjustedCharge(PercentCharge) * _maxJumpVelocity;
                    _coyoteTimeHandler.Value.ResetCoyoteTime();
                }
                OnExecuteChargeJump?.Invoke(GetAdjustedCharge(PercentCharge), _isGrounded.Value);

                _charging = false;
                _chargeTimer = 0;
            }
        }
    }

    float GetAdjustedCharge(float rawCharge)
    {
        return _jumpVelocityCurve.Evaluate(rawCharge);
    }

}
