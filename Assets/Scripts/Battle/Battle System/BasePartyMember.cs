using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BasePartyMember : ScriptableObject
{
    [field: SerializeField]
    [field: Required]
    public Sprite BattleSprite {get; private set;}

    public abstract float InitialHP {get;}

    public abstract float HP {get; set;}

    public abstract float MP {get; set;}

    public abstract List<BattleMove> Moves {get;}

    public abstract BattleStats BattleStats {get;}

    public abstract string Name {get;}

    public virtual List<BattleMove> GetAvailableMoves(BattleUnit user, BattleContext context)
    {
        List<BattleMove> moves = new();
        
        foreach (var move in Moves)
        {
            if (move.CanBeUsed(user, context))
                moves.Add(move);
        }

        return moves;
    }
}