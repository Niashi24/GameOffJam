using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MoveSelection2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    [Required]
    Image _outline;

    [SerializeField]
    Color _disabled, _enabled;

    [SerializeField]
    [Required]
    Text _nameField;

    [SerializeField]
    [Required]
    Text _descriptionField;

    public BattleMove Move {get; private set;}

    //might add a sfx for this later
    public Action OnEnableOutline;

    public Action<MoveSelection2> OnClick;
    public Action<MoveSelection2> OnEnter;
    public Action<MoveSelection2> OnExit;

    void OnDisable()
    {
        DisableOutline();
    }

    void OnEnable()
    {
        DisableOutline();
    }

    [Button]
    public void SetMove(BattleMove move)
    {
        Move = move;
        _nameField.text = move.MoveName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableOutline();
        OnEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableOutline();
        OnEnter?.Invoke(this);
    }

    public void EnableOutline()
    {
        _outline.color = _enabled;
        _descriptionField.text = Move.MoveDescription;
        OnEnableOutline?.Invoke();
    }

    public void DisableOutline()
    {
        _outline.color = _disabled;
    }
}
