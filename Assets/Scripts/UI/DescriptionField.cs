using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionField : MonoBehaviour
{
    [SerializeField]
    Text _textField;

    [SerializeField]
    Image _background;

    private bool _isActive = false;
    public bool isActive => _isActive;

    public void SetText(string text)
    {
        _textField.text = text;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;

        _background.enabled = isActive;
        _textField.enabled = isActive;
    }
}
