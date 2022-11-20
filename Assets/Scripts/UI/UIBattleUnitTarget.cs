using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBattleUnitTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    UITargetSelector _targetGroup;

    [SerializeField]
    Camera _camera;

    public BattleUnit Target {get; private set;}

    [SerializeField]
    [Required]
    Image _outline;

    [SerializeField]
    Color _disabled, _enabled;

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
    public void SetTarget(BattleUnit target)
    {
        Target = target;
        transform.position = _camera.WorldToScreenPoint(target.transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _targetGroup.SelectTarget(Target);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableOutline();
    }

    [Button]
    public void EnableOutline()
    {
        _outline.color = _enabled;
        _descriptionField.text = Target.BaseMember.Name;
        OnEnableOutline?.Invoke();
    }

    [Button]
    public void DisableOutline()
    {
        _outline.color = _disabled;
    }
}