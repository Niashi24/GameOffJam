using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Party Member")]
public class PlayerPartyMember : BasePartyMember
{
    public System.Action<float> OnHPChange;
    public System.Action<float> OnMPChange;

    [SerializeField]
    string _name;
    public override string Name => _name;

    [SerializeField]
    int _level;
    public int Level => _level;

    [SerializeField]
    float _hp;

    [SerializeField]
    float _mp;

    public override float InitialHP => BattleStats.HP;

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

    public override float MP
    {
        get => _mp;
        set
        {
            _mp = value;
            OnMPChange?.Invoke(_mp);
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
    [ButtonGroup("ResetStats")]
    [DisableIf("@_playerBase == null")]
    public void ResetHP()
    {
        if (_playerBase is null) return;
        _hp = _battleStats.HP;
    }

    [Button]
    [ButtonGroup("ResetStats")]
    [DisableIf("@_playerBase == null")]
    public void ResetMP()
    {
        if (_playerBase is null) return;
        _mp = _battleStats.MP;
    }
}
