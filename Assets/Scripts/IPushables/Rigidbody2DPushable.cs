using UnityEngine;
using Sirenix.OdinInspector;

public class Rigidbody2DPushable : IPushable
{
    Rigidbody2D _rbdy2D;

    public Rigidbody2DPushable(Rigidbody2D rbdy)
    {
        this._rbdy2D = rbdy;
    }

    public void Push(Vector2 force)
    {
        _rbdy2D.velocity += force/_rbdy2D.mass;
    }
}
