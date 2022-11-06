using System.Collections;
using UnityEngine;

public abstract class PlayerStatusCondition : ScriptableObject
{
    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, PlayerMove playerMove);
    //called after playing the attack
    //goes from 0-1
    public virtual float getAttackScore() => 1;
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, PlayerMove playerMove, float attackScore);
} 