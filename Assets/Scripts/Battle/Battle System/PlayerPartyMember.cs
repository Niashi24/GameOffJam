using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Party Member")]
public class PlayerPartyMember : BasePartyMember
{
    public System.Action<float> OnHPChange;

    [SerializeField]
    int _level;
    public int Level => _level;

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
        return _playerBase.GetAdjustedStats(_level);
    }

    [Button]
    [DisableIf("@_playerBase == null")]
    public void ResetHP()
    {
        if (_playerBase is null) return;
        _hp = _playerBase.GetAdjustedStats(_level).HP;
    }
}
