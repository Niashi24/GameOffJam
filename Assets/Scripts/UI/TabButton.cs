using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    [Required]
    TabGroup _tabGroup;

    [SerializeField]
    [Required]
    Image _background;
    public Image Background => _background;

    [SerializeField]
    GameObject _page;

    public UnityEvent<TabButton> OnTabSelected, OnTabDeselected;

    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _tabGroup.Subscribe(this);
    }
    
    public void Select()
    {
        if (_page != null)
            _page.SetActive(true);
        OnTabSelected?.Invoke(this);
    }

    public void Deselect()
    {
        if (_page != null)
            _page.SetActive(false);
        OnTabDeselected?.Invoke(this);
    }
}
