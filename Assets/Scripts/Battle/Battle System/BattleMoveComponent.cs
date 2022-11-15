using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BattleMoveComponent : MonoBehaviour
{
    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, BattleAttack battleAttack);
    //called after playing the attack
    //goes from 0-1
    public abstract float getAttackScore();
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, BattleAttack battleAttack, float attackScore);

    #if UNITY_EDITOR
    [Button]
    [DisableInEditorMode]
    private void TestMove()
    {
        StartCoroutine(TestMoveCoroutine());
    }

    private IEnumerator TestMoveCoroutine()
    {
        yield return this.PlayAttack(null, null);
        float attackScore = this.getAttackScore();
        Debug.Log(attackScore);
        // yield return this.PlayEffect(null, null, attackScore);
    }
    #endif
}