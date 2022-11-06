using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpiderVisual : MonoBehaviour
{
    [SerializeField]
    PlayerController _controller;
    [SerializeField]
    SpiderGrappeler _grappeler;
    [SerializeField]
    LineRenderer _lineRenderer;

    [Header("Colors")]
    [SerializeField]
    Color _pullingColor;
    
    [SerializeField]
    Color _swingingColor;

    [ShowInInspector, ReadOnly]
    public static readonly Color DEFAULT_COLOR = new Color(1,1,1,0.5f);

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
