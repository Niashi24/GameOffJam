using System.Collections;
using System.Collections.Generic;

public interface IBattleAttackChooser
{
    //Coroutine that entails the actual choosing of attacks over time
    IEnumerator WaitToChooseAttacks();
    //Returns Attacks (must have callen WaitToChooseAttacks first)
    List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager);
}