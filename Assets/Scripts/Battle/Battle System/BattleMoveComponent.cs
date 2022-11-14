using System.Collections;
using UnityEngine;

public abstract class BattleMoveComponent : MonoBehaviour
{
    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, BattleMove playerMove);
    //called after playing the attack
    //goes from 0-1
    public abstract float getAttackScore();
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, BattleMove playerMove, float attackScore);
}