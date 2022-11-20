using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiMoveDisplayer : IMoveDisplayer
{
    [SerializeReference]
    List<IMoveDisplayer> _moveDisplayers;

    public void DisplayMoves(List<BattleMove> moves)
    {
        foreach (var displayer in _moveDisplayers)
            displayer.DisplayMoves(moves);
    }
}
