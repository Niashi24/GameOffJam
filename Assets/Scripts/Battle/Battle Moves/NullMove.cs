using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullMove : BattleMoveComponent
{

    public override IEnumerator PlayAttack(BattleContext context, BattleAttack battleAttack)
    {
        Debug.LogError("Played Null Move Attack.");
        yield break;
    }

    public override float GetAttackScore()
    {
        Debug.LogError("Tried to get attack score of Null Move.");
        return 0;
    }

    public override IEnumerator PlayEffect(BattleContext context, BattleAttack battleAttack, float attackScore)
    {
        Debug.LogError("Played Null Move effect.");
        yield break;
    }

    public override List<BattleUnit> GetTargetableUnits(BattleUnit user, BattleContext context)
    {
        List<BattleUnit> targetableUnits = new();
        targetableUnits.AddRange(context.PlayerUnitManager.ActiveUnits);
        targetableUnits.AddRange(context.EnemyUnitManager.ActiveUnits);
        return targetableUnits;
    }

    public override bool CanBeUsed(BattleUnit user, BattleContext context)
    {
        return GetTargetableUnits(user, context).Count > 0;
    }
}
