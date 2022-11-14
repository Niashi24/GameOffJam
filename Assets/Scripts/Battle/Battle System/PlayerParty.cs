using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Battle System/Player Party")]
public class PlayerParty : ScriptableObject
{
    [SerializeField]
    List<PlayerPartyMember> _partyMembers;

    public Action<PlayerPartyMember> OnAddPartyMember;

    [Button]
    public void AddPartyMember(PlayerPartyMember member)
    {
        _partyMembers.Add(member);

        OnAddPartyMember?.Invoke(member);
    }
}