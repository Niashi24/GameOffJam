using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [SerializeField]
    float _minX = -100;
    [SerializeField]
    float _maxX = 100;

    [SerializeField]
    float _smoothTime = 0.3f;

    [ShowInInspector, ReadOnly]
    private float targetX = 0;

    [ShowInInspector, ReadOnly]
    private float velocity = 0;

    void Start()
    {
        targetX = transform.localPosition.x;
    }

    void Update()
    {
        transform.localPosition = transform.localPosition.With(
            x: Mathf.SmoothDamp(transform.localPosition.x, targetX, ref velocity, _smoothTime)
        );
    }

    [Button]
    [DisableInEditorMode]
    public void SetTargetX(float x)
    {
        targetX = Mathf.Clamp(x, _minX, _maxX);
    }

    [Button]
    [DisableInEditorMode]
    public void SetTargetTransformX(Transform target)
    {
        SetTargetX(target.position.x - transform.parent.position.x);
    }
}
