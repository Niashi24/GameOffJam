using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AllUnit : BattleUnit
{
    private List<BattleUnit> units;
    [ShowInInspector, ReadOnly]
    public List<BattleUnit> Units => units;

    [SerializeField]
    string _name = "All";

    public override float InitialHP => 0;
    public override float InitialMP => 0;

    public override float HP 
    { 
        get
        {
            if (units is null) return default;

            float hp = 0;
            foreach (var unit in units)
                hp += unit.HP;
            return hp;
        } 
        set
        {
            Debug.LogError("Error! Tried to set HP of AllUnit.", this);
        }
    }

    public override float MP 
    { 
        get 
        {
            if (units is null) return default;

            float mp = 0;
            foreach (var unit in units)
                mp += unit.MP;
            return mp;
        }
        set => Debug.LogError("Error! Tried to set MP of AllUnit.", this); 
    }

    public override List<BattleMove> Moves => new();

    public override List<BattleMove> GetAvailableMoves(BattleUnit user, BattleContext context) => new();

    public override void DealDamage(BattleAttack playerAttack, float attackScore)
    {
        if (units is null) return;
        foreach (var unit in units)
        {
            BattleAttack attack = new()
            {
                User = playerAttack.User,
                Target = unit,
                MoveBase = playerAttack.MoveBase
            };
            unit.DealDamage(attack, attackScore);
        }
    }

    protected override BattleStats BaseStats => BattleStats.zero;
    public override void SetPartyMember(BasePartyMember member) {}

    public void SetUnits(List<BattleUnit> units)
    {
        this.units = units;
    }

    void OnDrawGizmos()
    {
        Color before = Gizmos.color;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, Vector3.one*16);
        Gizmos.color = before;
    }

    public override bool CanAttack => false;

    public override string Name => _name;
}
