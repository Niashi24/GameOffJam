using System.Collections.Generic;
using UnityEngine;

public abstract class BasePartyMember : ScriptableObject
{
    public abstract float InitialHP {get;}

    public abstract float HP {get; set;}

    public abstract List<BattleMove> GetAttacks();

    public abstract BattleStats GetStats();
}