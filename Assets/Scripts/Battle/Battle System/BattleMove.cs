using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Battle Move")]
public class BattleMove : ScriptableObject
{
    [SerializeField]
    BattleMoveComponent _movePrefab;

    [SerializeField]
    MoveType _moveType;
    public MoveType MoveType => _moveType;

    [SerializeField]
    string _moveName;
    public string MoveName => _moveName;
    
    [SerializeField]
    [TextArea(3, int.MaxValue)]
    string _moveDescription;
    public string MoveDescription => _moveDescription;

    [SerializeField]
    float _moveMultiplier;
    public float MoveMultiplier => _moveMultiplier;

    public IEnumerator PlayMove(BattleContext context, BattleAttack battleAttack, Vector3? position = null, Transform parent = null)
    {
        position = position ?? Vector3.zero;
        BattleMoveComponent battleMove;
        if (parent != null)
            battleMove = Instantiate(_movePrefab, position.Value, Quaternion.identity, parent);
        else 
            battleMove = Instantiate(_movePrefab, position.Value, Quaternion.identity);

        yield return battleMove.PlayAttack(context, battleAttack);
        float attackScore = battleMove.GetAttackScore();
        yield return battleMove.PlayEffect(context, battleAttack, attackScore);

        //clean up move
        Destroy(battleMove);
    }

    public List<BattleUnit> GetTargetableUnits(BattleContext context)
    {
        return _movePrefab.GetTargetableUnits(context);
    } 
}
