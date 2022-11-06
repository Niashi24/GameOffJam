using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BatShooter : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    [Required]
    Camera _camera;

    [SerializeField]
    [Required]
    [AssetsOnly]
    BatProjectileScript _projectilePrefab;

    [SerializeField]
    AnimationCurve _projectileVelocityCurve;

    [SerializeField]
    float _maxVelocity = 150;

    [SerializeField]
    float _timeToCharge;

    [ShowInInspector, ReadOnly]
    float chargeTimer;

    [ShowInInspector, ReadOnly]
    bool charging;

    [ShowInInspector]
    public Action OnFullCharge, OnStartCharge, OnFire;

    public bool Charging => charging;
    public float ChargePercent 
    {
        get
        {
            if (_timeToCharge == 0) return 1;
            return chargeTimer/_timeToCharge;
        }
    }

    void OnEnable()
    {
        _input.OnLeftMouseDown += StartCharge;
        _input.OnLeftMouseUp += Fire; 
    }

    void OnDisable()
    {
        _input.OnLeftMouseDown -= StartCharge;
        _input.OnLeftMouseUp -= Fire; 
    }

    void Update()
    {
        if (!charging) return;

        if (chargeTimer < _timeToCharge)
        {
            chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, _timeToCharge);
            
            if (chargeTimer == _timeToCharge)
                OnFullCharge?.Invoke();
        }
    }

    void StartCharge()
    {
        charging = true;
    }

    void Fire()
    {
        float percent = GetAdjustedPercent(ChargePercent);

        var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, transform.parent);
        projectile.Rigidbody2D.velocity = _camera.GetUnitVectorFromPositionToMouse(transform.position) * (percent * _maxVelocity);

        charging = false;
        chargeTimer = 0;
    }

    float GetAdjustedPercent(float percent)
    {
        return _projectileVelocityCurve.Evaluate(percent);
    }
}
