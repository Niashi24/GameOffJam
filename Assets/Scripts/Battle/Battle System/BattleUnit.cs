using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class BattleUnit : MonoBehaviour
{
    public abstract float InitialHP {get;}

    public abstract float HP {get; set;}

    protected BasePartyMember _baseMember;
    [ShowInInspector, ReadOnly]
    public BasePartyMember BaseMember => _baseMember;

    public Action<float> OnHPChange;

    public virtual void SetPartyMember(BasePartyMember member)
    {
        _baseMember = member;
    }

    public BattleStats GetBattleStats()
    {
        //TODO: could possibly adjust due to status ailments
        BattleStats baseStats = BaseMember.BattleStats;

        return baseStats;
    }

    public void DealDamage(float damage)
    {
        HP = Mathf.Max(0, HP - damage);
    }
}
