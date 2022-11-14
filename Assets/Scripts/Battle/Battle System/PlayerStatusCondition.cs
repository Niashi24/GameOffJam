using System.Collections;
using UnityEngine;

public abstract class BattleStatusCondition : ScriptableObject
{
    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, BattleUnit unit);
    //called after playing the attack
    //goes from 0-1
    public virtual float getAttackScore() => 1;
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, BattleUnit unit, float attackScore);
} 