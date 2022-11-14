using System.Collections.Generic;

public class PlayerUnit : BattleUnit
{

    List<BattleStatusCondition> statusConditions = new();
    List<BattleStatusCondition> StatusConditions => statusConditions;

    public override float HP
    {
        get => BaseMember.HP;
        set 
        {
            BaseMember.HP = value;
            this.OnHPChange?.Invoke(BaseMember.HP);
        }
    }

    public override float InitialHP => BaseMember.HP;

    public override void SetPartyMember(BasePartyMember member)
    {
        base.SetPartyMember(member);
    }

    public void AddStatusCondition(BattleStatusCondition condition)
    {
        statusConditions.Add(condition);
    }
}
