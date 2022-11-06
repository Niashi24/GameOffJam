using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitManager : MonoBehaviour
{
    [SerializeField]
    List<PlayerUnit> _playerUnits;

    public List<PlayerUnit> PlayerUnits => _playerUnits;

    internal void InitializePlayerUnits(PlayerParty playerParty)
    {
        throw new NotImplementedException();
    }
}
