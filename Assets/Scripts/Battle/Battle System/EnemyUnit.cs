using UnityEngine;

public class EnemyUnit : BattleUnit
{
    float _hp = 0;
    public override float HP
    {
        get => _hp;

        set
        {
            _hp = value;
            base.OnHPChange?.Invoke(_hp);
        }
    }
    public override float InitialHP => _baseMember.GetStats().HP;

    public override void SetPartyMember(BasePartyMember member)
    {
        base.SetPartyMember(member);
        ResetUnit();
    }
    
    public void ResetUnit()
    {
        _hp = InitialHP;
    }
}