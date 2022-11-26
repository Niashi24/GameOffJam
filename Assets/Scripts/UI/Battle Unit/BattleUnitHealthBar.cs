using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleUnitHealthBar : MonoBehaviour
{
    [SerializeField]
    [Required]
    BattleUnit _battleUnit;

    [SerializeReference]
    IValueBar _healthBar;
    //Stack that tells it to be either enabled or disabled
    Stack<bool> displayStack = new Stack<bool>();

    void OnEnable()
    {
        UpdateUI();
        _battleUnit.OnHPChange += UpdateUI;
    }

    void OnDisable()
    {
        _battleUnit.OnHPChange -= UpdateUI;
    }

    //just ignore new value and use the stuff in _battleUnit
    void UpdateUI(float HP) => UpdateUI();

    void UpdateUI()
    {
        if (_battleUnit is null) return;
        if (_healthBar is null) return;

        _healthBar.SetMaxValue(_battleUnit.GetBattleStats().HP);
        _healthBar.SetValue(_battleUnit.HP);
    }

    public void Push(bool value)
    {
        displayStack.Push(value);
        gameObject.SetActive(value);
    }

    public void Pop()
    {
        displayStack.Pop();
        if (displayStack.TryPeek(out bool active))
            gameObject.SetActive(active);
        else
            gameObject.SetActive(false);
    }
}
