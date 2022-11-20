using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Enemy Party Member")]
public class EnemyPartyMember : BasePartyMember
{
    [SerializeField]
    string _name;
    public override string Name => _name;

    [SerializeField]
    EnemyBase _enemyBase;


    public override float InitialHP => _enemyBase.BaseStats.HP;
    public override float HP
    {
        get => InitialHP;
        set
        {
            Debug.LogError("Error! Tried to set HP of EnemyPartyMember");
        }
    }

    public override BattleStats BattleStats => _enemyBase.BaseStats;

    public override List<BattleMove> Moves => _enemyBase.Moves;
}
