using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Battle Move")]
public class BattleMove : ScriptableObject
{
    [SerializeField]
    BattleMoveComponent _movePrefab;

    public IEnumerator PlayMove(BattleContext context, BattleAttack battleAttack, Vector3? position = null, Transform parent = null)
    {
        position = position ?? Vector3.zero;
        BattleMoveComponent battleMove;
        if (parent != null)
            battleMove = Instantiate(_movePrefab, position.Value, Quaternion.identity, parent);
        else 
            battleMove = Instantiate(_movePrefab, position.Value, Quaternion.identity);

        yield return battleMove.PlayAttack(context, battleAttack);
        float attackScore = battleMove.getAttackScore();
        yield return battleMove.PlayEffect(context, battleAttack, attackScore);

        //clean up move
        Destroy(battleMove);
    }
}