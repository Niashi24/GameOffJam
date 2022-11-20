using System.Collections;

public class BattleStatusCondition
{
    public StatusCondition baseCondition;

    public BattleStatusCondition(StatusCondition baseCondition)
    {
        this.baseCondition = baseCondition;
    }

    //plays out the animation
    public IEnumerator PlayAttack(BattleContext context, BattleUnit unit)
    {
        if (baseCondition is null) yield break;
        yield return baseCondition.PlayAttack(context, unit);
    }
    //called after playing the attack
    //goes from 0-1
    public virtual float getAttackScore() => 1;
    //plays the effect after the attack, doing any damage/status conditions, and playing animations
    public IEnumerator PlayEffect(BattleContext context, BattleUnit unit)
    {
        if (baseCondition is null) yield break;
        yield return baseCondition.PlayEffect(context, unit, getAttackScore());
    }
    //process the stats
    public BattleStats ProcessStats(BattleStats stats)
    {
        if (baseCondition is null) return stats;
        return baseCondition.ProcessStats(stats);
    }
} 
