using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Battle System/Player Base")]
public class PlayerBase : SerializedScriptableObject
{
    [SerializeField]
    BattleStats _baseStats;
    public BattleStats BaseStats => _baseStats;

    [SerializeField]
    Dictionary<int, PlayerMove> _attacksByLevel;

    public List<PlayerMove> GetAttacksWithLevel(int level)
    {
        List<PlayerMove> output = new();
        foreach (var (lvl, atk) in _attacksByLevel)
        {
            if (level >= lvl)
                output.Add(atk);
        }
        return output;
    }
}
