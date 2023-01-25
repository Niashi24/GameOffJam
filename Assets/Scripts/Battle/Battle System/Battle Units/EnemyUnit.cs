using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyUnit : BattleUnit
{
    private BasePartyMember _baseMember;
    public BasePartyMember BaseMember => _baseMember;

    float _hp = 0;
    float _mp = 0;

    public override float HP
    {
        get => _hp;

        set
        {
            _hp = value;
            base.OnHPChange?.Invoke(_hp);
        }
    }

    public override float MP
    {
        get => _mp;
        set
        {
            _mp = value;
            base.OnMPChange?.Invoke(_mp);
        }
    }

    public override float InitialHP => _baseMember.BattleStats.HP;
    public override float InitialMP => _baseMember.BattleStats.MP;

    public override List<BattleMove> Moves => _baseMember.Moves;
    public override List<BattleMove> GetAvailableMoves(BattleUnit user, BattleContext context)
    {
        return _baseMember.GetAvailableMoves(user, context);
    }

    protected override BattleStats BaseStats
    {
        get 
        {
            if (_baseMember is null) return BattleStats.zero;
            return _baseMember.BattleStats;
        }
    }

    public override void DealDamage(BattleAttack playerAttack, float attackScore)
    {
        HP = Mathf.Max(0, HP - DamageCalculator.CalculateDamage(playerAttack, attackScore));
    }

    public override void SetPartyMember(BasePartyMember member)
    {
        _baseMember = member;
        ResetUnit();
        OnSetPartyMember?.Invoke(this, member);
    }
    
    public void ResetUnit()
    {
        _hp = InitialHP;
        _mp = InitialMP;
    }

    void OnDrawGizmos()
    {
        Color before = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one*16);
        Gizmos.color = before;
    }
    
    public override string Name => _baseMember.Name;
}
