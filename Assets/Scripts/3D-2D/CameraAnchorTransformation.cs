using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
public class CameraAnchorTransformation : MonoBehaviour
{
    [SerializeField]
    Camera _inputCamera;

    [SerializeField]
    Camera _outputCamera;

    [SerializeField]
    Transform _anchor;

    //The relative position on the current the anchor will set the position to
    [SerializeField]
    [Range(-1, 1)]
    float _anchorX = 0;
    [SerializeField]
    [Range(-1, 1)]
    float _anchorY = 0;

    [SerializeReference]
    [Tooltip("The size used when calculating the position of the anchor")]
    ISize _sizeReference = new ManualSize();

    [SerializeField]
    [Tooltip("Anchor position is transferred from Input Camera using Input type into Middle Type")]
    CameraPositionType _inputCameraType;
    [SerializeField]
    CameraPositionType _middleCameraType = CameraPositionType.SCREEN_POINT;
    [SerializeField]
    [Tooltip("Translated position is transferred from Middle Type into output position using Output Camera")]
    CameraPositionType _outputCameraType;

    void LateUpdate()
    {
        if (_inputCamera == null) return;
        if (_outputCamera == null) return;
        if (_anchor == null) return;
        if (_sizeReference == null) _sizeReference = new ManualSize();

        Vector2 size = _sizeReference.Size;
        Vector2 halfSize = size / 2;

        var inputFunction = CameraTransformationToFunction(_inputCameraType, _middleCameraType);
        var outputFunction = CameraTransformationToFunction(_middleCameraType, _outputCameraType);

        Vector3 position = outputFunction(inputFunction(_anchor.position, _inputCamera), _outputCamera);
        position += new Vector3(-_anchorX * halfSize.x, -_anchorY * halfSize.y);

        transform.position = position;
    }

    public void SetAnchor(Transform anchor)
    {
        this._anchor = anchor;
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

    public enum CameraPositionType {WORLD, SCREEN_POINT, VIEWPORT};

    private static readonly Dictionary<(CameraPositionType, CameraPositionType), Func<Vector3, Camera, Vector3>> inputOutputTypeToFunction = new()
    {
        {(CameraPositionType.WORLD, CameraPositionType.SCREEN_POINT), WorldToScreenPoint},
        {(CameraPositionType.WORLD, CameraPositionType.VIEWPORT), WorldToViewport},
        {(CameraPositionType.SCREEN_POINT, CameraPositionType.WORLD), ScreenPointToWorld},
        {(CameraPositionType.SCREEN_POINT, CameraPositionType.VIEWPORT), ScreenPointToViewport},
        {(CameraPositionType.VIEWPORT, CameraPositionType.WORLD), ViewportToWorldPoint},
        {(CameraPositionType.VIEWPORT, CameraPositionType.SCREEN_POINT), ViewportToScreenPoint}
    };

    private static readonly Func<Vector3, Camera, Vector3> defaultIdentityFunction = (x,y) => x;
    Func<Vector3, Camera, Vector3> CameraTransformationToFunction(CameraPositionType input, CameraPositionType output)
    {
        if (inputOutputTypeToFunction.ContainsKey((input, output))) return inputOutputTypeToFunction[(input, output)];
        return defaultIdentityFunction;
    }

    private static Vector3 WorldToScreenPoint(Vector3 pos, Camera camera) => camera.WorldToScreenPoint(pos);
    private static Vector3 WorldToViewport(Vector3 pos, Camera camera) => camera.WorldToViewportPoint(pos);
    private static Vector3 ScreenPointToWorld(Vector3 pos, Camera camera) => camera.ScreenToWorldPoint(pos);
    private static Vector3 ScreenPointToViewport(Vector3 pos, Camera camera) => camera.ScreenToViewportPoint(pos);
    private static Vector3 ViewportToWorldPoint(Vector3 pos, Camera camera) => camera.ViewportToWorldPoint(pos);
    private static Vector3 ViewportToScreenPoint(Vector3 pos, Camera camera) => camera.ViewportToScreenPoint(pos);
}