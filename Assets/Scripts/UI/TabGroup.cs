using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class TabGroup : MonoBehaviour
{
    private List<TabButton> tabButtons;
    public List<TabButton> TabButtons => tabButtons;

    [SerializeField]
    [Required]
    Sprite _tabIdle, _tabHover, _tabActive;

    [SerializeField]
    TabButton _defaultTab;

    private TabButton _selectedTab;
    public TabButton SelectedTab => _selectedTab;

    public Action<TabButton> OnTabSelected;

    public void Subscribe(TabButton button)
    {
        if (tabButtons is null)
            tabButtons = new List<TabButton>();

        if (_selectedTab == null && _defaultTab == button)
            OnTabClicked(button);

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (_selectedTab is null || button != _selectedTab)
        {
            button.Background.sprite = _tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabClicked(TabButton button)
    {
        _selectedTab?.Deselect();
        _selectedTab = button;
        _selectedTab.Select();

        ResetTabs();
        button.Background.sprite = _tabActive;

        OnTabSelected?.Invoke(button);
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (_selectedTab is not null && _selectedTab == button) continue;
            button.Background.sprite = _tabIdle;
        }
    }
}
