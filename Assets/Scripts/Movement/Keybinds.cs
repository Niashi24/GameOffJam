using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Keybinds")]
public class Keybinds : ScriptableObject
{
    [SerializeField]
    KeyCode _up = KeyCode.LeftArrow;
    public KeyCode Up => _up;

    [SerializeField]
    KeyCode _down = KeyCode.DownArrow;
    public KeyCode Down => _down;

    [SerializeField]
    KeyCode _left = KeyCode.LeftArrow;
    public KeyCode Left => _left;

    [SerializeField]
    KeyCode _right = KeyCode.RightArrow;
    public KeyCode Right => _right;

    [SerializeField]
    KeyCode _confirm = KeyCode.Z;
    public KeyCode Confirm => _confirm;

    [SerializeField]
    KeyCode _back = KeyCode.X;
    public KeyCode Back => _back;

    [SerializeField]
    KeyCode _shift = KeyCode.LeftShift;
    public KeyCode Shift => _shift;
}