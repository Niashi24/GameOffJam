using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using LS.Utilities;

public class CustomDistanceJoint : MonoBehaviour
{

    [SerializeField]
    [Required]
    ValueReference<Rigidbody2D> _playerRigidbody;

    [SerializeField]
    [Required]
    ValueReference<Transform> _grappleAnchorTransform;

    [SerializeField]
    [Required]
    ObjectValueReference<IPushable> _pushable;

    [SerializeField]
    bool _maxDistanceOnly = true;

    [SerializeField]
    float _maxAccelerationPerFrame = 100;

    public const float MIN_DISTANCE = 0.01f;

    public System.Action OnOverAccelerationFrameLimit;

    void FixedUpdate()
    {
        if (_playerRigidbody.Value == null) return;
        if (_grappleAnchorTransform.Value == null) return;

        Vector2 uA = ((Vector2)_grappleAnchorTransform.Value.position - _playerRigidbody.Value.position).normalized;
        float vT2 = _playerRigidbody.Value.velocity.sqrMagnitude - Mathf.Pow(Vector2.Dot(uA, _playerRigidbody.Value.velocity),2);
        float r = Vector2.Distance(_playerRigidbody.Value.position, _grappleAnchorTransform.Value.position);
        if (r < MIN_DISTANCE)
            return;
        // Vector2 gravityForce = playerRigidbody.gravityScale * Physics2D.gravity;
        Vector2 aC = vT2 / r * uA * Time.deltaTime;// - gravityForce;

        //dot product
        float dp = Vector2.Dot(uA, _playerRigidbody.Value.velocity);
        Vector2 vN = uA * dp;
        if (_maxDistanceOnly)
        {
            if (dp < 0) //moving away from radius
                aC -= vN;
        }
        else
            aC -= vN;
            
        if (aC.magnitude > _maxAccelerationPerFrame)
        {
            aC = aC.WithMagnitude(_maxAccelerationPerFrame);
            OnOverAccelerationFrameLimit?.Invoke();
        }

        _playerRigidbody.Value.velocity += aC;
        _pushable.Value.Push(-aC * _playerRigidbody.Value.mass);
        // Debug.DrawRay(_playerRigidbody.Value.position, aC, Color.red, Time.deltaTime);
        // Debug.Log($"vT2: {vT2}, vN: {vN.magnitude}, r: {r}, aC {aC.magnitude}");
    }

    void OnDrawGizmos()
    {
        if (_playerRigidbody.Value == null || _grappleAnchorTransform.Value == null)
            return;
        float r = Vector2.Distance(_playerRigidbody.Value.position, _grappleAnchorTransform.Value.position);
        Gizmos.DrawWireSphere(_grappleAnchorTransform.Value.position, r);
    }
}
