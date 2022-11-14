using System.Collections;
using System.Collections.Generic;

public interface IPlayerAttackChooser
{
    IEnumerator Wait();

    List<BattleAttack> ChooseAttacks(PlayerUnitManager playerUnits);
}
