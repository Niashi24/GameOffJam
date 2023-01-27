using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using System.Collections;

public class MoveSelectionManager2 : SerializedMonoBehaviour, IMoveSelector
{
    [SerializeField]
    Dictionary<TabButton, MoveType> _tabMoveTypeDictionary;

    [SerializeField]
    [Required]
    TabGroup _tabGroup;

    [SerializeField]
    List<MoveSelection2> _moveSelections;

    [SerializeField]
    Keybinds _keybinds;

    List<BattleMove> currentMoves;

    BattleMove selectedMove;

    int selectionsUsed = 0;
    int lastSelectionIndex = 0;

    // Reverse of serialized dictionary
    private Dictionary<MoveType, TabButton> moveTypeTabDictionary;

    void OnEnable()
    {
        _tabGroup.OnTabSelected += OnTabSelected;
        
        foreach (var selection in _moveSelections)
        {
            selection.OnClick += OnMoveSelected;
        }
    }

    void OnDisable()
    {
        _tabGroup.OnTabSelected -= OnTabSelected;
        
        foreach (var selection in _moveSelections)
        {
            selection.OnClick -= OnMoveSelected;
        }
    }

    void Start()
    {
        moveTypeTabDictionary = _tabMoveTypeDictionary.ToDictionary(x => x.Value, x => x.Key);
    }

    public void DisplayMoves(List<BattleMove> moves)
    {
        currentMoves = moves;
        DisplayMoveType(MoveType.Attack);
    }

    public IEnumerator WaitForCoroutine(List<BattleMove> moves)
    {
        selectedMove = null;
        DisplayMoves(moves);

        void DoIfInputDown(KeyCode key, System.Action func)
        {
            if (Input.GetKeyDown(key)) func?.Invoke();
        }
        
        while (selectedMove == null)
        {
            if (Input.GetKeyDown(_keybinds.Confirm))
            {
                selectedMove = _moveSelections[lastSelectionIndex].Move;
                break;
            }
            else if (Input.GetKeyDown(_keybinds.Back))
            {
                // Exiting the coroutine before setting the selectedMove
                // will cause the "Value" property to return null, which will
                // let the UIMoveSelector know to cancel the current selection
                break;
            }
            
            DoIfInputDown(_keybinds.Right, () => CycleTabSelection(1));
            DoIfInputDown(_keybinds.Left, () => CycleTabSelection(-1));
            DoIfInputDown(_keybinds.Up, () => CycleMoveSelection(-1));
            DoIfInputDown(_keybinds.Down, () => CycleMoveSelection(1));
            
            yield return null;
        }

        yield break;
    }

    public BattleMove Value => selectedMove;

    public void DisplayMoveType(MoveType moveType)
    {
        _moveSelections.ForEach(x => x.gameObject.SetActive(false));

        selectionsUsed = 0;
        foreach (BattleMove move in currentMoves.Where(x => x.MoveType == moveType))
        {
            if (selectionsUsed >= _moveSelections.Count)
            {
                Debug.LogError($"Not enough Move Selections to show all moves of the type.");
                break;
            }

            _moveSelections[selectionsUsed].SetMove(move);
            _moveSelections[selectionsUsed].gameObject.SetActive(true);
            
            selectionsUsed++;
        }
    }

    public void OnTabSelected(TabButton moveTab)
    {
        if (!_tabMoveTypeDictionary.ContainsKey(moveTab)) return;
        if (currentMoves == null) return;

        MoveType type = _tabMoveTypeDictionary[moveTab];
        DisplayMoveType(type);
    }

    [Button]
    [DisableIf("@!isActiveAndEnabled")]
    public void CycleTabSelection(int i)
    {
        MoveType activeType = _tabMoveTypeDictionary[_tabGroup.SelectedTab];
        int moveInt = (int)activeType;
        moveInt += i;
        moveInt %= 4;
        if (moveInt < 0)
            moveInt += 4;

        activeType = (MoveType) moveInt;

        _tabGroup.OnTabClicked(moveTypeTabDictionary[activeType]);
    }

    public void OnMoveSelected(MoveSelection2 moveSelection)
    {
        // Once the selected move is assigned, the Coroutine will exit
        // Note, if the Move on the move selection is missing
        // then the move cannot be selected
        selectedMove = moveSelection.Move;
    }

    public void OnMoveEnter(MoveSelection2 moveSelection)
    {
        lastSelectionIndex = _moveSelections.IndexOf(moveSelection);
    }

    [Button]
    [DisableIf("@!isActiveAndEnabled")]
    public void CycleMoveSelection(int i)
    {
        if (selectionsUsed == 0) return;
        _moveSelections[lastSelectionIndex].OnPointerExit(null);
        
        lastSelectionIndex += i;
        lastSelectionIndex %= selectionsUsed;
        if (lastSelectionIndex < 0)
            lastSelectionIndex += selectionsUsed;

        _moveSelections[lastSelectionIndex].OnPointerEnter(null);
    }
}