using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Camera3DTo2D : MonoBehaviour
{
    [Header("3D")]

    [SerializeField]
    Camera _3DCamera;

    [SerializeField]
    Transform _3DAnchor;

    [Header("2D")]

    [SerializeField]
    Camera _2DCamera;

    [SerializeField]
    float _z = 0;

    [SerializeField]
    TransformationAnchor _anchorPosition = TransformationAnchor.MIDDLE;

    [SerializeReference]
    [Tooltip("The size used when calculating the position of the anchor")]
    ISize _sizeReference = new ManualSize();

    void LateUpdate()
    {
        if (_3DCamera == null) return;
        if (_2DCamera == null) return;
        if (_3DAnchor == null) return;
        if (_sizeReference == null) _sizeReference = new ManualSize();

        int type = ((int)_anchorPosition);
        int xType = type % 3 - 1; //left = -1, middle = 0, right = 1
        int yType = type / 3 - 1; //top = -1, middle = 0, bottom = 1

        Vector2 size = _sizeReference.Size;
        Vector2 halfSize = size / 2;

        Vector3 position = _2DCamera.ViewportToWorldPoint(_3DCamera.WorldToViewportPoint(_3DAnchor.position))
            .With(z: _z);

        position += new Vector3(-xType * halfSize.x, yType * halfSize.y);

        transform.position = position;
    }

    void OnGizmosDrawSelected()
    {
        if (_3DAnchor == null) return;
        Debug.DrawLine(transform.position, _3DAnchor.position, Color.green, Time.deltaTime);
    }

    public enum TransformationAnchor
    {
        TOP_LEFT = 0, TOP_MIDDLE = 1, TOP_RIGHT = 2,
        MIDDLE_LEFT = 3, MIDDLE = 4, MIDDLE_RIGHT = 5,
        BOTTOM_LEFT = 6, BOTTOM_MIDDLE = 7, BOTTOM_RIGHT = 8
    }

    [ContextMenu("Use Manual Size")]
    void SetToManual()
    {
        _sizeReference = new ManualSize();
    }

    [ContextMenu("Use Sprite Renderer Size")]
    void SetToSpriteRenderer()
    {
        _sizeReference = new SpriteRendererSize(GetComponent<SpriteRenderer>());
    }
}
