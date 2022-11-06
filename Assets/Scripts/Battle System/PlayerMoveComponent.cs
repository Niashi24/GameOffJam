using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMoveComponent : MonoBehaviour
{
    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, PlayerMove playerMove);
    //called after playing the attack
    //goes from 0-1
    public abstract float getAttackScore();
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, PlayerMove playerMove, float attackScore);
}