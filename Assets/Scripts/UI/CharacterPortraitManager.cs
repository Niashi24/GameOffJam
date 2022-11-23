using System.Collections.Generic;
using UnityEngine;

public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField]
    List<CharacterPortrait> _portraits;

    [SerializeField]
    BattleUnitManager _unitManager;

    void OnEnable()
    {
        _unitManager.OnInitializeBattleUnits += SetUnits;
    }

    void OnDisable()
    {
        _unitManager.OnInitializeBattleUnits -= SetUnits;
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
}