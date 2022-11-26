using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBattleUnitTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    Camera _camera;

    [ShowInInspector, ReadOnly]
    public UITargetSelector TargetGroup {get; private set;}

    [ShowInInspector, ReadOnly]
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
    public void SetTarget(BattleUnit target, UITargetSelector targetGroup)
    {
        Target = target;
        TargetGroup = targetGroup;
        transform.position = _camera.WorldToScreenPoint(target.transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
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
        _descriptionField.text = Target.Name;
        OnEnableOutline?.Invoke();
    }

    [Button]
    public void DisableOutline()
    {
        _outline.color = _disabled;
    }

    public void Select()
    {
        DisableOutline();
        TargetGroup?.SelectTarget(Target);
    }
}