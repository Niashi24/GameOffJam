using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MovePageDisplayer : MonoBehaviour, IMoveDisplayer
{
    [SerializeField]
    List<MoveSelection> _moveSelections;

    [SerializeField]
    MoveType _typeDisplay;

    [SerializeField]
    [Required]
    TabButton _button;

    int numCurrentMoves = 0;
    [ShowInInspector, ReadOnly]
    public int NumberOfCurrentMoves => numCurrentMoves;

    public Action OnPageSelected;

    void OnEnable()
    {
        _button.OnTabSelected.AddListener(OnTabSelected);
    }

    void OnDisable()
    {
        _button.OnTabSelected.RemoveListener(OnTabSelected);
    }

    private void OnTabSelected(TabButton button)
    {
        OnPageSelected?.Invoke();
    }

    public void DisplayMoves(List<BattleMove> moves)
    {
        _moveSelections.ForEach(x => x.gameObject.SetActive(false));

        numCurrentMoves = 0;
        int selectionIndex = 0;

        foreach (BattleMove move in moves)
        {
            if (selectionIndex >= _moveSelections.Count) 
            {
                Debug.LogError("Error! Ran out of selections", this);
                break;
            }

            if (move.MoveType == _typeDisplay)
            {
                _moveSelections[selectionIndex].SetMove(move);
                _moveSelections[selectionIndex].gameObject.SetActive(true);
                selectionIndex++;
                numCurrentMoves++;
            }
        }
    }

    public void Enable()
    {
        _button.OnPointerClick(null);
    }

    public void Enter(int index)
    {
        if (!IsInBounds(index)) return;

        _moveSelections[index].OnPointerEnter(null);
    }

    public void Exit(int index)
    {
        if (!IsInBounds(index)) return;

        _moveSelections[index].OnPointerExit(null);
    }

    public void Select(int index)
    {
        if (!IsInBounds(index)) return;

        _moveSelections[index].OnPointerClick(null);
    }

    public bool IsInBounds(int index)
    {
        return index >= 0 && index < NumberOfCurrentMoves;
    }
}
