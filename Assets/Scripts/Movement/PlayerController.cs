using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LS.Utilities;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D rbdy2D;

    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    ObjectReference<ICoyoteTime> _coyoteTime;

    [Header("Movement")]
    [SerializeField]
    float _maxRunSpeed = 20;
    [SerializeField]
    float _timeToAccelerate = 1;
    [SerializeField]
    float _timeToStop = 0.1f;

    [SerializeField]
    float _jumpVelocity = 100f;

    [Header("Collision")]
    [SerializeField]
    ValueReference<bool> _isGrounded;

    void OnEnable() 
    {
        _input.OnSpaceBarDown += Jump;    
    }

    void OnDisable()
    {
        _input.OnSpaceBarDown -= Jump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Ground Movement
        Movement();
    }

    private void Movement()
    {
        float xDir = _input.xDir;
        bool isGrounded = _isGrounded.Value;

        float maxSpeed = _maxRunSpeed;

        float targetVelocity = xDir * maxSpeed;
        
        if (xDir == 0)
            if (isGrounded)
                targetVelocity = 0;
            else
                targetVelocity = rbdy2D.velocity.x;
        

        float accelerationTime = xDir != 0 ? _timeToAccelerate : _timeToStop;
        rbdy2D.velocity += Vector2.right * (targetVelocity - rbdy2D.velocity.x) / accelerationTime * Time.deltaTime;
    }

    void Jump()
    {
        if (!_isGrounded.Value) return;
        if (_jumpVelocity == 0) return;

        rbdy2D.velocity = rbdy2D.velocity.With(y:_jumpVelocity);
        _coyoteTime.Value.ResetCoyoteTime();
    }
}
