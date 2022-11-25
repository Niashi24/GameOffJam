using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    [Required]
    UIMoveSelector _moveGroup;

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
        Move = move;
        _nameField.text = move.MoveName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _moveGroup.SelectMove(Move);
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
        _descriptionField.text = Move.MoveDescription;
        OnEnableOutline?.Invoke();
    }

    public void DisableOutline()
    {
        _outline.color = _disabled;
    }
}
