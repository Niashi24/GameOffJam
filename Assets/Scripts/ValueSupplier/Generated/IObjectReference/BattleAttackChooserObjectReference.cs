using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using UnityEngine;

[System.Serializable]
public class BattleAttackChooserObjectReference : IBattleAttackChooser
{
    [SerializeField]
    ObjectReference<IBattleAttackChooser> _battleAttackChooser;

    public IEnumerator WaitToChooseAttacks() => _battleAttackChooser.Value.WaitToChooseAttacks();
    public List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager) => _battleAttackChooser.Value.ChooseAttacks(unitManager);

}
