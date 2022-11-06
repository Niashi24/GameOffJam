using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpiderGrappeler : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    [Required]
    Camera _camera;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    [Required]
    CustomDistanceJoint _distanceJoint;

    [SerializeField]
    [Required]
    GrapplePoint _grapplePoint;
    public GrapplePoint GrapplePoint => _grapplePoint;

    [SerializeField]
    float _webSpeed = 100f;

    [SerializeField]
    float _pullAcceleration = 20f;

    [ShowInInspector, ReadOnly]
    public bool ShotGrapple {get; private set;}

    [ShowInInspector, ReadOnly]
    public bool Attached => ShotGrapple && _grapplePoint.Attached;

    [ShowInInspector, ReadOnly]
    public bool Swinging => Attached && _input.Shift;
    
    [ShowInInspector, ReadOnly]
    public bool Pulling => Attached && _input.LeftMouseHeld && !_input.Shift;

    void OnEnable() 
    {
        _input.OnRightMouseDown += ShootGrapple;
        _input.OnRightMouseUp += ResetGrapple;
        _distanceJoint.OnOverAccelerationFrameLimit += ResetGrapple;
        _grapplePoint.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        _input.OnRightMouseDown -= ShootGrapple;
        _input.OnRightMouseUp -= ResetGrapple;
        _distanceJoint.OnOverAccelerationFrameLimit -= ResetGrapple;
        ResetGrapple();
    }

    void FixedUpdate()
    {
        if (ShotGrapple)
        {

            if (_grapplePoint.transform.parent == null)
            {
                _grapplePoint.gameObject.SetActive(false);

                ShotGrapple = false;
            }
        }

        if (Pulling)
            PullTowardsGrapple();

        _distanceJoint.enabled = Swinging;
        // _distanceJoint.connectedAnchor = (_grapplePoint.transform.position - transform.position)/2;
    }

    void ShootGrapple()
    {
        ShotGrapple = true;
        _grapplePoint.gameObject.SetActive(true);
        _grapplePoint.StartShoot(
            transform.position, 
            _camera.GetUnitVectorFromPositionToMouse(transform.position) * _webSpeed
        );
    }

    void ResetGrapple()
    {
        ShotGrapple = false;
        _grapplePoint.gameObject.SetActive(false);
        _grapplePoint.ResetGrapple();
    }

    void PullTowardsGrapple()
    {

        Vector2 dir = ((Vector2)(_grapplePoint.transform.position - transform.position)).normalized;

        Vector2 accel = dir * _pullAcceleration * Time.deltaTime;

        _rbdy2D.velocity += accel;
        _grapplePoint.Push(-accel);

        // if (Swinging)
        //     _distanceJoint.connectedBody.AddForceAtPosition(-dir * _pullAcceleration * Time.deltaTime * _rbdy2D.mass, _grapplePoint.Rigidbody.position);
    }

    Vector2 RayFromPlayerToMouse()
    {
        return ((Vector2)(_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
    }
}
