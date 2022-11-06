using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BatModeController : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerController _playerController;
    
    [SerializeField]
    [Required]
    BatFlyController _batFlyController;

    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    private float _initialGravityScale = 1;

    void OnEnable()
    {
        _initialGravityScale = _rbdy2D.gravityScale;
    }

    void LateUpdate() 
    {
        _playerController.enabled = !_input.Shift;
        _batFlyController.enabled = _input.Shift;
        _rbdy2D.gravityScale = _input.Shift ? 0 : _initialGravityScale;
    }
}
