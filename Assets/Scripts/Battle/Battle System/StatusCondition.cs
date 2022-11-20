using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Status Condition")]
public abstract class StatusCondition : ScriptableObject
{
    [SerializeField]
    string _name;
    public string Name => _name;

    [SerializeField]
    [TextArea(3, int.MaxValue)]
    string _description;

    //plays out the animation
    public abstract IEnumerator PlayAttack(BattleContext context, BattleUnit unit);
    //called after playing the attack
    //goes from 0-1
    public virtual float getAttackScore() => 1;
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public abstract IEnumerator PlayEffect(BattleContext context, BattleUnit unit, float attackScore = 1);
    //process the stats
    public abstract BattleStats ProcessStats(BattleStats stats);
}