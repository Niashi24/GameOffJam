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
}
