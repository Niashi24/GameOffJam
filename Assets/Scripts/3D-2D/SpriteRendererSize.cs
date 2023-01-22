using UnityEngine;

[System.Serializable]
public class SpriteRendererSize : ISize
{
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    public SpriteRendererSize() {}
    
    public SpriteRendererSize(SpriteRenderer spriteRenderer)
    {
        this._spriteRenderer = spriteRenderer;
    }

    public Vector2 Size => _spriteRenderer != null ? _spriteRenderer.bounds.size : Vector2.one;
}