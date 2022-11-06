using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProjectileScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    float _maxTime;

    public Rigidbody2D Rigidbody2D => _rbdy2D;

    public Action OnCollide;

    void Start()
    {
        Invoke(nameof(DestroySelf), _maxTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DestroySelf();
    }

    void DestroySelf()
    {
        OnCollide?.Invoke();
        Destroy(gameObject);
    }
}
