using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    public static float CalculateDamage(BattleAttack playerAttack, float attackScore)
    {
        return playerAttack.User.GetBattleStats().Attack / playerAttack.Target.GetBattleStats().Defense * playerAttack.MoveBase.MoveMultiplier * attackScore;
    }

    public static float CalculateDamageMultipliers(BattleAttack playerAttack, float attackScore)
    {
        //potential todo: elemental damage multipliers?
        return 1;
    }
}
