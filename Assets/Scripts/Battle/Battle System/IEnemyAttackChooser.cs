using System.Collections;
using System.Collections.Generic;

public interface IEnemyAttackChooser
{
    IEnumerator Wait();

    List<EnemyAttack> ChooseAttacks(EnemyUnitManager playerUnits);
}
