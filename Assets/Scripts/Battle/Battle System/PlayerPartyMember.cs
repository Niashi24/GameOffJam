using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Party Member")]
public class PlayerPartyMember : BasePartyMember
{
    public System.Action<float> OnHPChange;

    [SerializeField]
    int _level;

    [SerializeField]
    float _hp;
    public override float InitialHP => _hp;

    public override float HP
    {
        get => _hp;
        set
        {
            _hp = value;
            OnHPChange?.Invoke(_hp);
        }
    }

    [SerializeField]
    PlayerBase _playerBase;

    public override List<BattleMove> GetAttacks()
    {
        return _playerBase.GetAttacksWithLevel(_level);
    }

    public override BattleStats GetStats()
    {
        //TODO: Take into account the level
        return _playerBase.BaseStats;
    }

    public void ResetHP()
    {

    }
}
