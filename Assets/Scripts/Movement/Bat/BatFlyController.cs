using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LS.Utilities;

public class BatFlyController : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    float _flightSpeed;

    void FixedUpdate()
    {
        _rbdy2D.velocity = _input.direction.normalized * _flightSpeed;
    }    
}
