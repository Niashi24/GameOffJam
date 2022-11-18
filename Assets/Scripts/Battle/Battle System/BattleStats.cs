using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStats
{
    [Min(0)]
    public float HP;
    [Min(0)]
    public float Attack;
    [Min(0)]
    public float Defense;
    [Min(0)]
    public float Quickness;

    public static readonly BattleStats zero = new BattleStats()
    {
        HP = 0,
        Attack = 0,
        Defense = 0,
        Quickness = 0
    };

    public static BattleStats operator +(BattleStats a, BattleStats b)
    {
        return new BattleStats()
        {
            HP = a.HP + b.HP,
            Attack = a.Attack + b.Attack,
            Defense = a.Defense + b.Defense,
            Quickness = a.Quickness + b.Quickness
        };
    }
}
