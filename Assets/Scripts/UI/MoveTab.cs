using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MoveTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [field: SerializeField]
    public MoveType MoveType {get; private set;} = MoveType.Attack;

    [Header("Text")]
    [SerializeField]
    [Required]
    Text _text;

    [SerializeField]
    Vector3 _activeTextOffset = Vector3.up * 6;

    private Vector3 originalPosition;

    [Header("Background Sprite")]

    [SerializeField]
    [Required]
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    [Required]
    Sprite _inactive, _highlighted, _active;

    public bool isActive {get; private set;}

    void Start()
    {
        originalPosition = _text.transform.localPosition;
    }

    public Action<MoveTab> OnEnter, OnClick, OnExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke(this);
    }

    public void Deactivate()
    {
        _spriteRenderer.sprite = _inactive; 
        _text.transform.localPosition = originalPosition;
        isActive = false;
    }

    public void SetHighlight(bool highlighted)
    {
        // _spriteRenderer.sprite = isActive ? true : _highlighted;
        _text.transform.localPosition = originalPosition;
        isActive = false;
    }

    public void Activate()
    {
        _spriteRenderer.sprite = _active;
        _text.transform.localPosition = originalPosition + _activeTextOffset;
        isActive = true;
    }
}