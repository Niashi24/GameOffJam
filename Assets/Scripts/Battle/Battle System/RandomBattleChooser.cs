using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBattleChooser : IBattleAttackChooser
{
    public List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager, BattleContext context)
    {
        List<BattleAttack> attacks = new();

        foreach (var unit in unitManager.ActiveUnits)
            attacks.Add(GetRandomAttack(unit, context));

        return attacks;
    }

    public IEnumerator WaitToChooseAttacks(BattleUnitManager unitManager, BattleContext context)
    {
        yield break;
    }

    BattleAttack GetRandomAttack(BattleUnit unit, BattleContext context)
    {
        List<BattleMove> moves = unit.GetAvailableMoves(unit, context);
        BattleMove move = moves[Random.Range(0, moves.Count)];
        List<BattleUnit> targets = move.GetTargetableUnits(unit, context);
        BattleUnit target = targets[Random.Range(0, targets.Count)];

        return new BattleAttack()
        {
            MoveBase = move,
            User = unit,
            Target = target
        };
    }
}
