using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    public static float CalculateDamage(BattleAttack playerAttack, float attackScore)
    {
        float userAttack = playerAttack.User.GetBattleStats().Attack;
        float targetDefense = Mathf.Max(1, playerAttack.Target.GetBattleStats().Defense);

        float calc = userAttack / targetDefense * playerAttack.MoveBase.MoveMultiplier * attackScore;
        calc = Mathf.Max(1, Mathf.Floor(calc));
        return calc;
    }

    public static float CalculateDamageMultipliers(BattleAttack playerAttack, float attackScore)
    {
        //potential todo: elemental damage multipliers?
        return 1;
    }
}
