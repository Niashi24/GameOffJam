using UnityEngine;

public class EnemyUnit : BattleUnit
{
    float _hp = 0;
    public float HP => _hp;
    
    EnemyPartyMember _basePlayer;
    public EnemyPartyMember BaseEnemy => _basePlayer;

    public void SetPartyMember(EnemyPartyMember member)
    {
        this._basePlayer = member;
        ResetUnit();
    }
    
    public void ResetUnit()
    {
        _hp = _basePlayer.GetStats().HP;
    }
}
