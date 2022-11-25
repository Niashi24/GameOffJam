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

    [SerializeField]
    [Required]
    UIBar _magicBar;

    [SerializeField]
    [Required]
    Text _magicText;

    [SerializeField]
    [Required]
    Image _background;

    [SerializeField]
    Color _disabledColor = Color.gray;

    [SerializeField]
    Color _enabledColor = Color.white;

    [SerializeField]
    [Required]
    GameObject _toDisable;

    [SerializeField]
    [Required]
    GameObject _highlight;

    [ShowInInspector, ReadOnly]
    public bool Enabled {get; private set;}
    [ShowInInspector, ReadOnly]
    public bool Highlighted {get; private set;}

    [ShowInInspector, ReadOnly]
    public BattleUnit CurrentUnit {get; private set;} = null;

    public void SetUnit(BattleUnit unit)
    {
        ResetUnit();

        CurrentUnit = unit;

        _healthBar.SetMaxValue(CurrentUnit.GetBattleStats().HP);
        _healthBar.SetValue(CurrentUnit.HP);
        _magicBar.SetMaxValue(CurrentUnit.GetBattleStats().MP);
        _magicBar.SetValue(CurrentUnit.MP);

        CurrentUnit.OnHPChange += OnHPChange;
        CurrentUnit.OnMPChange += OnMPChange;

        _nameText.text = CurrentUnit.Name;
        _healthText.text = $"{CurrentUnit.HP}/{CurrentUnit.GetBattleStats().HP}";
        _magicText.text = $"{CurrentUnit.MP}/{CurrentUnit.GetBattleStats().MP}";
    }

    private void OnHPChange(float HP)
    {
        _healthBar.SetValue(HP);
        _healthText.text = $"{HP}/{CurrentUnit.GetBattleStats().HP}";
    }

    private void OnMPChange(float MP)
    {
        _magicBar.SetValue(MP);
        _healthText.text = $"{MP}/{CurrentUnit.GetBattleStats().MP}";
    }

    public void ResetUnit()
    {
        if (CurrentUnit is null) return;

        CurrentUnit.OnHPChange -= OnHPChange;
        CurrentUnit.OnMPChange -= OnMPChange;
        CurrentUnit = null;
    }

    [Button]
    public void Disable()
    {
        _background.color = _disabledColor;
        _toDisable.SetActive(false);
        Enabled = false;
    }

    [Button]
    public void Enable()
    {
        _background.color = _enabledColor;
        _toDisable.SetActive(true);
        Enabled = true;
    }

    public void EnableHighlight()
    {
        if (!Enabled) return;
        if (Highlighted) return;

        _highlight.SetActive(true);
        Highlighted = true;
    }

    public void DisableHighlight()
    {
        if (!Highlighted) return;
        
        _highlight.SetActive(false);
        Highlighted = false;
    }
}
