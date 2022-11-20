using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    UIMoveSelector _moveGroup;

    private BattleMove move;
    public BattleMove Move => move;

    [SerializeField]
    [Required]
    Image _outline;

    [SerializeField]
    Color _disabled, _enabled;

    [SerializeField]
    Text _nameField;

    [SerializeField]
    [Required]
    Text _descriptionField;

    //might add a sfx for this later
    public System.Action OnEnableOutline;

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
        this.move = move;
        _nameField.text = move.MoveName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _moveGroup.SelectMove(move);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableOutline();
    }

    public void EnableOutline()
    {
        _outline.color = _enabled;
        _descriptionField.text = move.MoveDescription;
        OnEnableOutline?.Invoke();
    }

    public void DisableOutline()
    {
        _outline.color = _disabled;
    }
}
