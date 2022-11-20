using System.Collections;
using System.Collections.Generic;

public interface IBattleAttackChooser
{
    //Coroutine that entails the actual choosing of attacks over time
    IEnumerator WaitToChooseAttacks(BattleUnitManager unitManager, BattleContext context);
    //Returns Attacks (must have callen WaitToChooseAttacks first)
    List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager, BattleContext context);
}