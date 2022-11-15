using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAttack
{
    public BattleMove MoveBase;
    public BattleUnit User;
    public BattleUnit Target;

    public IEnumerator PlayAttack(BattleContext context, Vector3? position = null, Transform attackParent = null)
    {
        yield return MoveBase.PlayMove(context, this, position, attackParent);
    }
}
