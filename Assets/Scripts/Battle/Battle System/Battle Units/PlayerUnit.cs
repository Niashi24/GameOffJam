using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerUnit : BattleUnit
{
    private BasePartyMember _baseMember;
    public BasePartyMember BaseMember => _baseMember;

    public override float HP
    {
        get
        {
            if (BaseMember is not null) return BaseMember.HP;
            return default;
        }
        set 
        {
            BaseMember.HP = value;
            this.OnHPChange?.Invoke(BaseMember.HP);
        }
    }

    public override float MP
    {
        get
        {
            if (BaseMember is not null) return BaseMember.MP;
            return default;
        }
        set
        {
            if (BaseMember is null) return;
            BaseMember.MP = value;
            this.OnMPChange?.Invoke(BaseMember.MP);
        }
    }

    protected override BattleStats BaseStats
    {
        get
        {
            if (_baseMember is null) return BattleStats.zero;
            return _baseMember.BattleStats;
        }
    }

    public override float InitialHP => BaseMember.BattleStats.HP;
    public override float InitialMP => BaseMember.BattleStats.MP;

    public override List<BattleMove> Moves => _baseMember.Moves;
    public override List<BattleMove> GetAvailableMoves(BattleUnit user, BattleContext context)
    {
        return _baseMember.GetAvailableMoves(user, context);
    }

    public override void SetPartyMember(BasePartyMember member)
    {
        _baseMember = member;
    }

    public override void DealDamage(BattleAttack playerAttack, float attackScore)
    {
        HP = Mathf.Max(0, HP - DamageCalculator.CalculateDamage(playerAttack, attackScore));
    }

    void OnDrawGizmos()
    {
        Color before = Gizmos.color;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one*16);
        Gizmos.color = before;
    }
    
    public override string Name => _baseMember.Name;
}
