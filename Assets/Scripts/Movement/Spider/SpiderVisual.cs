using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpiderVisual : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerController _controller;
    [SerializeField]
    [Required]
    SpiderGrappeler _grappeler;
    [SerializeField]
    [Required]
    LineRenderer _lineRenderer;
    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [Header("Colors")]
    [SerializeField]
    Color _pullingColor;
    
    [SerializeField]
    Color _swingingColor;

    [ShowInInspector, ReadOnly]
    public static readonly Color DEFAULT_COLOR = new Color(1,1,1,0.5f);

    private float targetZAngle;
    [SerializeField]
    float _angleAcceleration = 360;

    void Start()
    {
        _lineRenderer.SetPositions(new Vector3[2]);
        _lineRenderer.enabled = false;
    }

    void LateUpdate()
    {
        _lineRenderer.enabled = _grappeler.ShotGrapple;
        
        _lineRenderer.endColor = _lineRenderer.startColor = GetLineRendererColor();
        _lineRenderer.SetPositions(new Vector3[] {_grappeler.transform.position, _grappeler.GrapplePoint.transform.position});

        if ((_grappeler.Swinging || _grappeler.Pulling) && !_controller.isGrounded)
        {
            targetZAngle = ((Vector2)(_grappeler.GrapplePoint.transform.position - _grappeler.transform.position)).normalized.AngleFromHorizontal() * Mathf.Rad2Deg - 90;
        }
        else
        {
            targetZAngle = 0;
        }

        _spriteRenderer.transform.eulerAngles = _spriteRenderer.transform.eulerAngles.With(z:Mathf.MoveTowardsAngle(_spriteRenderer.transform.eulerAngles.z, targetZAngle, _angleAcceleration * Time.deltaTime));
    }

    Color GetLineRendererColor()
    {
        if (_grappeler.Pulling)
            return _pullingColor;
        if (_grappeler.Swinging)
            return _swingingColor;

        return DEFAULT_COLOR;
    }
}
