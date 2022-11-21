using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField]
    [Required]
    Text _nameText;

    [SerializeField]
    [Required] 
    UIBar _healthBar;

    [SerializeField]
    [Required]
    Text _healthText;

    [ShowInInspector, ReadOnly]
    public BattleUnit CurrentUnit {get; private set;} = null;

    public void SetUnit(BattleUnit unit)
    {
        ResetUnit();
        CurrentUnit = unit;
        _healthBar.SetMaxValue(CurrentUnit.GetBattleStats().HP);
        _healthBar.SetValue(CurrentUnit.HP);
        _nameText.text = CurrentUnit.BaseMember.Name;
        CurrentUnit.OnHPChange += OnHPChange;
        _healthText.text = $"{CurrentUnit.HP}/{CurrentUnit.GetBattleStats().HP}";
        
    }

    private void OnHPChange(float HP)
    {
        _healthBar.SetValue(HP);
        _healthText.text = $"{HP}/{CurrentUnit.GetBattleStats().HP}";
    }

    public void ResetUnit()
    {
        if (CurrentUnit is null) return;

        CurrentUnit.OnHPChange -= _healthBar.SetValue;
        CurrentUnit = null;
    }

    public void Disable()
    {
        //todo: implement
    }
}
