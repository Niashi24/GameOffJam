using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Party Member")]
public class PlayerPartyMember : BasePartyMember
{
    public System.Action<float> OnHPChange;

    [SerializeField]
    string _name;
    public override string Name => _name;

    [SerializeField]
    int _level;
    public int Level => _level;

    [SerializeField]
    float _hp;
    public override float InitialHP => _hp;

    [SerializeField]
    BattleStats _battleStats = BattleStats.zero;

    public override BattleStats BattleStats => _battleStats;

    [SerializeField]
    List<BattleMove> _moves;

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

    public override List<BattleMove> Moves => _moves;

    [Button]
    [DisableIf("@_playerBase == null")]
    public (BattleStats, List<BattleMove>) LevelUp()
    {
        //increment level
        _level++;
        //calculate stat increases
        List<BattleMove> newMoves = _playerBase.GetAttacksAtLevel(_level);
        BattleStats statIncrease = _playerBase.GetStatIncrease(_level);
        //apply stat increases
        _battleStats += statIncrease;
        _moves.AddRange(newMoves);
        //adjust hp
        _hp += statIncrease.HP;
        //return output
        return (statIncrease, newMoves);
    }

    [Button]
    [DisableIf("@_playerBase == null")]
    public void ResetHP()
    {
        if (_playerBase is null) return;
        _hp = _battleStats.HP;
    }
}
