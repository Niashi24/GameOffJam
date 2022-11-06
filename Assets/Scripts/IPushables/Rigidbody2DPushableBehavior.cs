using UnityEngine;
using Sirenix.OdinInspector;

public class Rigidbody2DPushableBehavior : MonoBehaviour, IPushable
{
    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    public void Push(Vector2 force)
    {
        _rbdy2D.velocity += force/_rbdy2D.mass;
        // Debug.Log($"Velocity: {force/_rbdy2D.mass}");
    }
}