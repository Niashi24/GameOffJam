using UnityEngine;

public class PressButtonsMoveButtonScript : MonoBehaviour
{
    bool pressed;
    public bool Pressed => pressed;

    public System.Action OnFirstPress;
    public System.Action OnButtonPress;

    public void ResetButton()
    {
        pressed = false;
        OnFirstPress = null;
        OnButtonPress = null;
    }

    public void OnPress()
    {
        if (!pressed)
        {
            pressed = true;
            OnFirstPress?.Invoke();
        }
        OnButtonPress?.Invoke();
    }
}