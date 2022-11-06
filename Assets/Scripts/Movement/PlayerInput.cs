using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlayerInput : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public float xDir {get; private set;} = 0;

    [ShowInInspector, ReadOnly]
    public float yDir {get; private set;} = 0;
    
    [ShowInInspector, ReadOnly]
    public Vector2 direction => new Vector2(xDir, yDir);

    [ShowInInspector, ReadOnly]
    public bool Shift {get; private set;} = false;

    [ShowInInspector, ReadOnly]
    public bool Space {get; private set;} = false;

    [ShowInInspector, ReadOnly]
    public bool LeftMouseHeld {get; private set;} = false;

    [ShowInInspector]
    public Action OnRightMouseDown, OnRightMouseUp;

    [ShowInInspector]
    public Action OnLeftMouseDown, OnLeftMouseUp;

    [ShowInInspector]
    public Action OnSpaceBarDown;

    [ShowInInspector]
    public Action OnPress1, OnPress2, OnPress3;

    void Update() {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        Shift = Input.GetKey(KeyCode.LeftShift);
        Space = Input.GetKey(KeyCode.Space);
        LeftMouseHeld = Input.GetMouseButton(0);

        TriggerIfDown(OnPress1, KeyCode.Alpha1);
        TriggerIfDown(OnPress2, KeyCode.Alpha2);
        TriggerIfDown(OnPress3, KeyCode.Alpha3);

        if (Input.GetMouseButtonDown(0))
            OnLeftMouseDown?.Invoke();
        if (Input.GetMouseButtonUp(0))
            OnLeftMouseUp?.Invoke();
            
        if (Input.GetMouseButtonDown(1))
            OnRightMouseDown?.Invoke();
        if (Input.GetMouseButtonUp(1))
            OnRightMouseUp?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            OnSpaceBarDown?.Invoke();
    }

    void TriggerIfDown(Action action, KeyCode key)
    {
        if (Input.GetKeyDown(key))
            action?.Invoke();
    }
}
