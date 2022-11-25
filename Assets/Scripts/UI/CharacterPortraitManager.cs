using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField]
    List<CharacterPortrait> _portraits;

    [SerializeField]
    [Required]
    BattleUnitManager _unitManager;

    [SerializeField]
    [Required]
    UIMoveSelector _moveSelector;

    void OnEnable()
    {
        _unitManager.OnInitializeBattleUnits += SetUnits;
        _moveSelector.OnStartCreateAttack += SetHighlight;
        _moveSelector.OnFinishCreateAttack += RemoveHighlight;
    }

    void OnDisable()
    {
        _unitManager.OnInitializeBattleUnits -= SetUnits;
        _moveSelector.OnStartCreateAttack -= SetHighlight;
        _moveSelector.OnFinishCreateAttack -= RemoveHighlight;
    }

    public void SetUnits(List<BattleUnit> units)
    {
        _portraits.ForEach(x => x.Disable());

        for (int i = 0; i < units.Count && i < _portraits.Count; i++)
        {
            _portraits[i].SetUnit(units[i]);
            _portraits[i].Enable();
        }

        if (units.Count > _portraits.Count)
        {
            Debug.LogError("Out of Character Portraits.", this);
        }
    }

    public void SetHighlight(BattleUnit unit)
    {
        if (TryGetPortraitOfUnit(out var portrait, unit))
        {
            portrait.EnableHighlight();
        }
    }

    public void RemoveHighlight(BattleUnit unit, bool successful)
    {
        if (TryGetPortraitOfUnit(out var portrait, unit))
        {
            portrait.DisableHighlight();
        }
    }

    public bool TryGetPortraitOfUnit(out CharacterPortrait portrait, BattleUnit unit)
    {
        portrait = GetPortraitOfUnit(unit);
        return portrait != null;
    }

    private CharacterPortrait GetPortraitOfUnit(BattleUnit unit)
    {
        foreach (var portrait in _portraits)
        {
            if (!portrait.Enabled) continue;

            if (portrait.CurrentUnit == unit)
                return portrait;
        }

        return null;
    }
}