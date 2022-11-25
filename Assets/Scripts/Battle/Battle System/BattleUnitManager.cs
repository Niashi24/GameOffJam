using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    [SerializeField]
    List<BattleUnit> _availableUnits;

    [SerializeField]
    [Required]
    AllUnit _allUnit;
    public AllUnit All => _allUnit;

    private List<BattleUnit> _activeUnits = new();

    public List<BattleUnit> ActiveUnits => _activeUnits;

    [SerializeReference]
    IBattleUnitPlacer _battleUnitPlacer = new NullBattleUnitPlacer();

    public System.Action<List<BattleUnit>> OnInitializeBattleUnits;

    public void InitializeBattleUnits(BattleParty battleParty)
    {
        _availableUnits.ForEach(x => x.gameObject.SetActive(false));
        _activeUnits.Clear();

        if (battleParty.PartyMembers.Count > _availableUnits.Count)
        {
            AddMoreUnits(battleParty.PartyMembers.Count - _availableUnits.Count);
        }

        for (int i = 0; i < battleParty.PartyMembers.Count; i++)
        {
            _activeUnits.Add(_availableUnits[i]);
            _activeUnits[i].SetPartyMember(battleParty.PartyMembers[i]);
            _activeUnits[i].gameObject.SetActive(true);
        }

        _allUnit.SetUnits(_activeUnits);
        _battleUnitPlacer?.PlaceUnits(_activeUnits);
        OnInitializeBattleUnits?.Invoke(ActiveUnits);
    }

    protected virtual void AddMoreUnits(int count)
    {
        Debug.LogError("Out of available units. Must create more.", this);
    }
}