using UnityEngine;
using LS.Utilities;
using Sirenix.OdinInspector;

public class BoxColliderGroundChecker : MonoBehaviour, IValueSupplier<bool>, ICoyoteTime
{
    [SerializeField]
    [Required]
    BoxCollider2D _collider;
    [SerializeField]
    LayerMask _layerMask;
    [SerializeField]
    float _dY = 0.001f;

    [SerializeField]
    float _coyoteTimeSeconds;

    [ShowInInspector, ReadOnly]
    float _coyoteTimer;

    void FixedUpdate()
    {
        _coyoteTimer = Mathf.Max(0, _coyoteTimer - Time.deltaTime);

        if (isGrounded())
            _coyoteTimer = _coyoteTimeSeconds;
    }

    private bool isGrounded()
    {
        if (_collider is null) return false;
        Vector2 pos = (Vector2) _collider.transform.position + Vector2.down * _dY;
        Vector2 size = _collider.size.ComponentMultiply((Vector2)transform.lossyScale);
        return Physics2D.OverlapBox(pos, size, 0, _layerMask) != null;
    }

    [ShowInInspector, ReadOnly]
    public bool Value
    {
        get 
        {
            return _coyoteTimer > 0;
        }
        set 
        {
            Debug.LogWarning("Why are you trying to set the value of a BoxColliderGroundChecker?", this);
        }
    }

    public void ResetCoyoteTime()
    {
        _coyoteTimer = 0;
    }

    void OnDrawGizmosSelected() 
    {
        if (_collider is null) return;

        Vector2 pos = (Vector2) _collider.transform.position + Vector2.down * _dY;
        Vector2 size = _collider.size.ComponentMultiply((Vector2)transform.lossyScale);
        Gizmos.DrawWireCube(pos, size);
    }
}
