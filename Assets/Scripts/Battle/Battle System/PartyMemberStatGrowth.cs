using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Party Member Stat Growth Curves")]
public class PartyMemberStatGrowth : ScriptableObject
{
    [SerializeField]
    AnimationCurve _HPCurve;

    [SerializeField]
    AnimationCurve _ATKCurve;

    [SerializeField]
    AnimationCurve _DEFCurve;

    [SerializeField]
    AnimationCurve _QUICKCurve;

    public BattleStats GetAdjustedStats(int level)
    {
        return new BattleStats()
        {
            HP = _HPCurve.Evaluate(level),
            Attack = _ATKCurve.Evaluate(level),
            Defense = _DEFCurve.Evaluate(level),
            Quickness = _QUICKCurve.Evaluate(level)
        };
    }
    
}