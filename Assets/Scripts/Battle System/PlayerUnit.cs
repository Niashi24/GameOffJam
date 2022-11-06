using System.Collections.Generic;

public class PlayerUnit : BattleUnit
{
    PlayerPartyMember _basePlayer;
    public PlayerPartyMember BasePlayer => _basePlayer;

    List<PlayerStatusCondition> statusConditions = new();
    List<PlayerStatusCondition> StatusConditions => statusConditions;

    public float HP => _basePlayer.HP;

    public void SetPartyMember(PlayerPartyMember member)
    {
        this._basePlayer = member;
    }

    public void AddStatusCondition(PlayerStatusCondition condition)
    {
        statusConditions.Add(condition);
    }
}
