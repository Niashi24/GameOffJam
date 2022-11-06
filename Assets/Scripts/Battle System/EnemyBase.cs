using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Enemy Base")]
public class EnemyBase : ScriptableObject
{
    [SerializeField]
    BattleStats _baseStats;
    public BattleStats BaseStats => _baseStats;

    [SerializeField]
    List<EnemyMove> _enemyMoves;

    public List<EnemyMove> GetMoves()
    {
        return _enemyMoves;
    }
}