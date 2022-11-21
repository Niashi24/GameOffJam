using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerUnit : BattleUnit
{
    public override float HP
    {
        get
        {
            if (BaseMember is not null) return BaseMember.HP;
            return default;
        }
        set 
        {
            BaseMember.HP = value;
            this.OnHPChange?.Invoke(BaseMember.HP);
        }
    }

    public override float InitialHP => BaseMember.BattleStats.HP;

    public override void SetPartyMember(BasePartyMember member)
    {
        base.SetPartyMember(member);
    }

    void OnDrawGizmos()
    {
        Color before = Gizmos.color;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one*16);
        Gizmos.color = before;
    }
}
