using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Battle System/Player Base")]
public class PlayerBase : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<int, BattleMove> _attacksByLevel;

    [SerializeField]
    PartyMemberStatGrowth _statGrowthCurve;

    [Button]
    public List<BattleMove> GetAttacksWithLevel(int level)
    {
        List<BattleMove> output = new();
        foreach (var (lvl, atk) in _attacksByLevel)
        {
            if (level >= lvl)
                output.Add(atk);
        }
        return output;
    }

    [Button]
    public List<BattleMove> GetAttacksAtLevel(int level)
    {
        List<BattleMove> output = new();
        if (_attacksByLevel is null)
            return output;
        foreach (var (lvl, atk) in _attacksByLevel)
            if (level == lvl)
                output.Add(atk);
        return output;
    }

    [Button]
    public BattleStats GetStatIncrease(int level)
    {
        if (_statGrowthCurve is null) return BattleStats.zero;
        return _statGrowthCurve.GetAdjustedStats(level);
    }
}
