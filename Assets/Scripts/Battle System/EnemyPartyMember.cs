using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Enemy Party Member")]
public class EnemyPartyMember : ScriptableObject
{
    [SerializeField]
    EnemyBase _enemyBase;

    public BattleStats GetStats()
    {
        return _enemyBase.BaseStats;
    }
}