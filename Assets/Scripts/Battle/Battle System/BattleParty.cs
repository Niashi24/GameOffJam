using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Battle System/Battle Party")]
public class BattleParty : ScriptableObject
{
    [SerializeField]
    List<BasePartyMember> _partyMembers;

    public List<BasePartyMember> PartyMembers => _partyMembers;

    public Action<BasePartyMember> OnAddPartyMember;

    [Button]
    public virtual void AddPartyMember(BasePartyMember member)
    {
        _partyMembers.Add(member);

        OnAddPartyMember?.Invoke(member);
    }
}