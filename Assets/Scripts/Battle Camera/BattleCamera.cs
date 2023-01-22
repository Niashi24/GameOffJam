using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [Header("Restrictions")]
    [SerializeField]
    float _minX = -100;
    [SerializeField]
    float _maxX = 100;
    [SerializeField]
    float _minY = -130;
    [SerializeField]
    float _maxY = -75;

    [SerializeField]
    float _smoothTime = 0.3f;

    [SerializeField]
    float _offsetZ = -130;

    [ShowInInspector, ReadOnly]
    private float targetX = 0;

    [ShowInInspector, ReadOnly]
    private float velocity = 0;

    [ShowInInspector, ReadOnly]
    private Vector3 vel = Vector2.zero;

    [ShowInInspector, ReadOnly]
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.localPosition;
    }

    void Update()
    {
        // transform.localPosition = transform.localPosition.With(
        //     x: Mathf.SmoothDamp(transform.localPosition.x, targetX, ref velocity, _smoothTime)
        // );

        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            targetPos,
            ref vel,
            _smoothTime
        );
    }

    [Button]
    [DisableInEditorMode]
    public void SetTargetLocation(Vector3 position)
    {
        targetPos = new Vector3(
            Mathf.Clamp(position.x, _minX, _maxX),
            transform.localPosition.y,
            Mathf.Clamp(position.z + _offsetZ, _minY, _maxY)
        );
    }

    [Button]
    [DisableInEditorMode]
    public void SetTargetTransform(Transform target)
    {
        SetTargetLocation(target.position - transform.parent.position);
    }
}
