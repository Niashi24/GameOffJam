using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BatVisual : MonoBehaviour
{
    [SerializeField]
    [Required]
    BatModeController _controller;
    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;
    [SerializeField]
    [Required]
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    float _maxTilt = 30;
    [SerializeField]
    float _maxSpeed = 100;

    [SerializeField]
    float _angleAcceleration = 360;

    [ShowInInspector, ReadOnly]
    private float targetZAngle;
    
    void LateUpdate()
    {
        targetZAngle = calcLogistic(_rbdy2D.velocity.x / _maxSpeed, _maxTilt);

        _spriteRenderer.transform.eulerAngles = _spriteRenderer.transform.eulerAngles.With(z:Mathf.MoveTowardsAngle(_spriteRenderer.transform.eulerAngles.z, targetZAngle, _angleAcceleration * Time.deltaTime));
    }

    float calcLogistic(float t, float L)
    {
        return (L/(1+Mathf.Exp(t)) - L/2) * 2;
    }
}
