using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBattleUnitTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // [SerializeField]
    // [Required]
    // Camera _inputCamera;

    // [SerializeField]
    // [Required]
    // Camera _outputCamera;

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
    DescriptionField _descriptionField;

    [SerializeField]
    [Required]
    RectTransform _rectTransform;

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

        UpdatePosition();
        // transform.position = _outputCamera.ViewportToScreenPoint(_inputCamera.WorldToViewportPoint(Target.transform.position));
    }

    void Update()
    {
        UpdatePosition();
        // transform.position = _outputCamera.ViewportToScreenPoint(_inputCamera.WorldToViewportPoint(Target.transform.position));
    }

    public void UpdatePosition()
    {
        if (Target == null) return;
        if (TargetGroup == null) return;

        Bounds bounds = Target.Bounds2D;
        transform.position = bounds.center + new Vector3(176, 120, 0);
        _rectTransform.sizeDelta = bounds.extents * 2;
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
        _descriptionField.SetText(Target.Name);
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