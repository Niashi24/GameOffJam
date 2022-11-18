using System.Collections.Generic;
using UnityEngine;

public abstract class BasePartyMember : ScriptableObject
{
    public abstract float InitialHP {get;}

    public abstract float HP {get; set;}

    // public abstract List<BattleMove> GetMoves();

    public abstract List<BattleMove> Moves {get;}

    public abstract BattleStats BattleStats {get;}
}