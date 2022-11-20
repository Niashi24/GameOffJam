using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePageDisplayer : MonoBehaviour, IMoveDisplayer
{
    [SerializeField]
    List<MoveSelection> _moveSelections;

    [SerializeField]
    MoveType _typeDisplay;

    public void DisplayMoves(List<BattleMove> moves)
    {
        _moveSelections.ForEach(x => x.gameObject.SetActive(false));

        int selectionIndex = 0;

        foreach (BattleMove move in moves)
        {
            if (selectionIndex >= _moveSelections.Count) break;

            if (move.MoveType == _typeDisplay)
            {
                _moveSelections[selectionIndex].SetMove(move);
                _moveSelections[selectionIndex].gameObject.SetActive(true);
                selectionIndex++;
            }
        }
    }
}
