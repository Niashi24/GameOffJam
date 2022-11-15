using System.Collections;
using System.Collections.Generic;

public interface IBattleAttackChooser
{
    IEnumerator Wait();

    List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager);
}