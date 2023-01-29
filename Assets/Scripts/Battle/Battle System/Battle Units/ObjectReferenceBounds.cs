using LS.Utilities;
using UnityEngine;

[System.Serializable]
public class ObjectReferenceBounds : IBounds
{
    [SerializeField]
    ObjectReference<IBounds> _boundsObjectReference;

    public Bounds Bounds2D
    {
        get
        {
            if (!_boundsObjectReference.HasValue) return default;
            return _boundsObjectReference.Value.Bounds2D;
        }
    }
}