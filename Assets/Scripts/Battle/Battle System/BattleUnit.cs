using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BattleUnit : MonoBehaviour
{
    public abstract float InitialHP {get;}

    public abstract float HP {get; set;}

    protected BasePartyMember _baseMember;
    public BasePartyMember BaseMember => _baseMember;

    public Action<float> OnHPChange;

    public virtual void SetPartyMember(BasePartyMember member)
    {
        _baseMember = member;
    }
}
