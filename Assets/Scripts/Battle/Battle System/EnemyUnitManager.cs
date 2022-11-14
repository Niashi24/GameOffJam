using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitManager : MonoBehaviour
{
    [SerializeField]
    List<EnemyUnit> _enemyUnits;

    public List<EnemyUnit> EnemyUnits => _enemyUnits;

    internal void InitializeEnemyUnits(EnemyParty enemyParty)
    {
        throw new NotImplementedException();
    }
}
