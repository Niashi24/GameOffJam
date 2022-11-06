using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Party Member")]
public class PlayerPartyMember : ScriptableObject
{
    [SerializeField]
    int _level;

    [SerializeField]
    float _hp;
    public float HP;

    [SerializeField]
    PlayerBase _playerBase;

    public List<PlayerMove> GetAttacks()
    {
        return _playerBase.GetAttacksWithLevel(_level);
    }

    public BattleStats GetStats()
    {
        //TODO: Take into account the level
        return _playerBase.BaseStats;
    }

    public void ResetHP()
    {

    }
}
