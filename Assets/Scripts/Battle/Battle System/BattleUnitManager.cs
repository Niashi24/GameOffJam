using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    [SerializeField]
    List<BattleUnit> _availableUnits;

    private List<BattleUnit> _activeUnits;

    public List<BattleUnit> ActiveUnits => _activeUnits;

    public void InitializeBattleUnits(BattleParty playerParty)
    {
        _availableUnits.ForEach(x => x.gameObject.SetActive(false));
        _activeUnits.Clear();

        if (playerParty.PartyMembers.Count > _availableUnits.Count)
        {
            AddMoreUnits(playerParty.PartyMembers.Count - _availableUnits.Count);
        }



        throw new NotImplementedException();
    }

    private void AddMoreUnits(int count)
    {
        Debug.LogError("Out of available units. Must create more.");
        throw new NotImplementedException();
    }
}