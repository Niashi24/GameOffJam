using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Enemy Party")]
public class EnemyParty : ScriptableObject
{
    [SerializeField]
    List<EnemyPartyMember> _partyMembers;

    public Action<EnemyPartyMember> OnAddPartyMember;

    [Button]
    public void AddPartyMember(EnemyPartyMember member)
    {
        _partyMembers.Add(member);

        OnAddPartyMember?.Invoke(member);  
    }
}