using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class TransformBounds : IBounds
{
    [SerializeField]
    [Required]
    Transform _transform;

    [SerializeField]
    Vector2 _extents;

    public Bounds Bounds2D
    {
        get
        {
            if (_transform == null) return default;
            return new Bounds(_transform.position, _extents);
        }
    }
}