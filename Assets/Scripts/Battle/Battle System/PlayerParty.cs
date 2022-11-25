using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// [CreateAssetMenu(menuName = "Battle System/Player Party")]
public class PlayerParty : BattleParty
{
    [Button]
    [ButtonGroup("ResetHPMP")]
    public void ResetHP() => DoToEachPlayer(x => x.ResetHP());

    [Button]
    [ButtonGroup("ResetHPMP")]
    public void ResetMP() => DoToEachPlayer(x => x.ResetMP());

    [Button]
    [ButtonGroup("ResetHPMP")]
    public void ResetHPMP() => DoToEachPlayer(x => {x.ResetHP(); x.ResetMP();});

    private void DoToEachPlayer(System.Action<PlayerPartyMember> action)
    {
        if (action == null) return;
        foreach (var partyMember in PartyMembers)
        {
            if (partyMember is PlayerPartyMember player)
            {
                action.Invoke(player);
            }
        }
    }
}
