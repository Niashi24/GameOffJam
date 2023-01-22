using UnityEngine;

[System.Serializable]
public class ManualSize : ISize
{
    [SerializeField]
    Vector2 _size;

    public Vector2 Size => _size;
}
