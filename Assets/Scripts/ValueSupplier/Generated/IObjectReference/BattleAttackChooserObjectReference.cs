using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class BattleAttackChooserObjectReference : IBattleAttackChooser
{
    [SerializeField]
    ObjectReference<IBattleAttackChooser> _battleAttackChooser;

    public IEnumerator WaitToChooseAttacks(BattleUnitManager unitManager, BattleContext context) => _battleAttackChooser.Value.WaitToChooseAttacks(unitManager, context);
    public List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager, BattleContext context) => _battleAttackChooser.Value.ChooseAttacks(unitManager, context);

}
