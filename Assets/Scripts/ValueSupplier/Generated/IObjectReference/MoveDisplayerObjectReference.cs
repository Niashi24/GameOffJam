using System.Collections.Generic;
using LS.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class MoveDisplayerObjectReference : IMoveDisplayer
{
    [SerializeField]
    ObjectReference<IMoveDisplayer> _moveDisplayer;

    public void DisplayMoves(List<BattleMove> moves)
    {
        _moveDisplayer.Value.DisplayMoves(moves);
    }
}