using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using LS.Utilities;

public class GrapplePoint : MonoBehaviour, IValueSupplier<IPushable>
{
    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    [Required]
    Collider2D _collider; //to disable when attached

    public Rigidbody2D Rigidbody => _rbdy2D;

    [ShowInInspector, ReadOnly]
    public bool Attached {get; private set;} = false;
    [ShowInInspector, ReadOnly]
    private Transform _attachedObject;

    private Vector2 prevPosition;

    [ShowInInspector, ReadOnly]
    public IPushable AttachedPushable {get; private set;} = NULL_PUSHABLE;
    //Implement IValueSupplier Interface
    public IPushable Value {get => AttachedPushable; set{}}

    private static readonly IPushable NULL_PUSHABLE = new NullPushable();

    [ShowInInspector]
    public Action<GameObject> OnAttachToObject;

    public void StartShoot(Vector2 position, Vector2 velocity)
    {
        _rbdy2D.isKinematic = false;
        _rbdy2D.velocity = velocity;
        transform.position = position;
        Attached = false;
    }

    public void ResetGrapple()
    {
        Attached = false;
        _attachedObject = null;
        _rbdy2D.velocity = Vector2.zero;
        _collider.enabled = true;
    }

    void LateUpdate()
    {
        if (_attachedObject == null) return;

        Vector3 displacement = _attachedObject.transform.position - (Vector3)prevPosition;
        transform.position += displacement;
        prevPosition = _attachedObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _rbdy2D.velocity = Vector2.zero;
        _rbdy2D.isKinematic = true;
        _attachedObject = other.transform;
        prevPosition = _attachedObject.position;
        Attached = true;
        _collider.enabled = false;

        AttachedPushable = GetPushableFromCollision(other);

        OnAttachToObject?.Invoke(other.gameObject);
    }

    IPushable GetPushableFromCollision(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IPushable>(out var pushable))
            return pushable;
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out var rbdy2D))
        {
            Debug.LogWarning("Missing rigidbodyPushable behavior on " + other.gameObject.name + ". Creating new.");
            return new Rigidbody2DPushable(rbdy2D);
        }
        return NULL_PUSHABLE;
    }

    public void Push(Vector2 force)
    {
        AttachedPushable.Push(force);
    }
}
