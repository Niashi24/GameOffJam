using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Enemy Base")]
public class EnemyBase : ScriptableObject
{
    [SerializeField]
    BattleStats _baseStats;
    public BattleStats BaseStats => _baseStats;

    [SerializeField]
    List<BattleMove> _battleMoves;

    public List<BattleMove> GetMoves()
    {
        return _battleMoves;
    }
}