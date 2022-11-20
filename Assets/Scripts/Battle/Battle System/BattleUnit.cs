using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class BattleUnit : MonoBehaviour
{
    public abstract float InitialHP {get;}

    [ShowInInspector, ReadOnly]
    public abstract float HP {get; set;}

    protected BasePartyMember _baseMember;
    [ShowInInspector, ReadOnly]
    public BasePartyMember BaseMember => _baseMember;

    List<BattleStatusCondition> statusConditions = new();
    [ShowInInspector, ReadOnly]
    List<BattleStatusCondition> StatusConditions => statusConditions;

    public Action<float> OnHPChange;

    public virtual void SetPartyMember(BasePartyMember member)
    {
        _baseMember = member;
    }

    public BattleStats GetBattleStats()
    {
        BattleStats outputStats = BaseMember.BattleStats;

        foreach (var statusCondition in statusConditions)
            outputStats = statusCondition.ProcessStats(outputStats);

        return outputStats;
    }

    public void DealDamage(float damage)
    {
        HP = Mathf.Max(0, HP - damage);
    }

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
