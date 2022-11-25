using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class BattleUnit : MonoBehaviour
{
    public abstract float InitialHP {get;}
    public abstract float InitialMP {get;}

    [ShowInInspector, ReadOnly]
    public abstract float HP {get; set;}

    [ShowInInspector, ReadOnly]
    public abstract float MP {get; set;}

    public abstract List<BattleMove> Moves {get;}
    public abstract List<BattleMove> GetAvailableMoves(BattleUnit user, BattleContext context);

    public abstract string Name {get;}

    public virtual bool CanAttack
    {
        get => HP > 0;
    }

    // protected BasePartyMember _baseMember;
    // [ShowInInspector, ReadOnly]
    // public BasePartyMember BaseMember => _baseMember;

    List<BattleStatusCondition> statusConditions = new();
    [ShowInInspector, ReadOnly]
    List<BattleStatusCondition> StatusConditions => statusConditions;

    public Action<float> OnHPChange;
    public Action<float> OnMPChange;

    public abstract void SetPartyMember(BasePartyMember member);
    protected abstract BattleStats BaseStats {get;}

    public BattleStats GetBattleStats()
    {
        BattleStats outputStats = BaseStats;

        foreach (var statusCondition in statusConditions)
            outputStats = statusCondition.ProcessStats(outputStats);

        return outputStats;
    }

    public void DealDamage(float damage)
    {
        HP = Mathf.Max(0, HP - damage);
    }

    public abstract void DealDamage(BattleAttack playerAttack, float attackScore);

    public void AddStatusCondition(BattleStatusCondition condition)
    {
        statusConditions.Add(condition);
    }

    public IEnumerator ProcessStatusConditions()
    {
        foreach (var statusCondition in statusConditions)
        {
            
        }
        yield break;
    }
    
}
