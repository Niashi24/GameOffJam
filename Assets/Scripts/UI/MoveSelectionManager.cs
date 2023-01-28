using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoveSelectionManager : MonoBehaviour, IMoveDisplayer
{
    [SerializeField]
    List<MovePageDisplayer> _pageDisplayers;

    [SerializeField]
    [Required]
    Keybinds _keybinds;

    [ShowInInspector, ReadOnly]
    int currentPageIndex = 0;

    [ShowInInspector, ReadOnly]
    int currentSelectionIndex = 0;

    [ShowInInspector, ReadOnly]
    public MovePageDisplayer currentDisplayer => _pageDisplayers[currentPageIndex];

    void OnEnable()
    {
        foreach (var page in _pageDisplayers)
            page.OnPageSelected += () => SelectPage(page);

        currentPageIndex = 0;
        currentSelectionIndex = 0;

        EnterCurrentSelection();
    }

    void OnDisable()
    {
        foreach (var page in _pageDisplayers)
            page.OnPageSelected -= () => SelectPage(page);
    }

    void SelectPage(MovePageDisplayer page)
    {
        currentPageIndex = _pageDisplayers.IndexOf(page);
    }

    public void DisplayMoves(List<BattleMove> moves)
    {
        foreach (MovePageDisplayer page in _pageDisplayers)
        {
            page.DisplayMoves(moves);
        }

        currentPageIndex = 0;
        currentSelectionIndex = 0;
        EnterCurrentSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(_keybinds.Right))
            MovePage(1);
        if (Input.GetKeyDown(_keybinds.Left))
            MovePage(-1);

        if (Input.GetKeyDown(_keybinds.Down))
            MoveSelection(1);
        if (Input.GetKeyDown(_keybinds.Up))
            MoveSelection(-1);

        if (Input.GetKeyDown(_keybinds.Confirm))
            SelectCurrentSelection();
    }

    void MovePage(int numPages)
    {
        if (numPages == 0) return;

        ExitCurrentSelection();

        currentPageIndex += numPages;

        while (currentPageIndex < 0) //loop if less
            currentPageIndex += _pageDisplayers.Count;
        if (_pageDisplayers.Count != 0) //avoid / by 0
            currentPageIndex = currentPageIndex % _pageDisplayers.Count; //loop if greater

        currentDisplayer.Enable();
        currentSelectionIndex = 0;
        EnterCurrentSelection();
    }

    void MoveSelection(int num)
    {
        if (num == 0) return;
        if (currentDisplayer.NumberOfCurrentMoves == 0) return;

        ExitCurrentSelection();

        currentSelectionIndex += num;

        // Loop around
        while (currentSelectionIndex < 0)
            currentSelectionIndex += currentDisplayer.NumberOfCurrentMoves;
        if (currentDisplayer.NumberOfCurrentMoves != 0) //avoid / by 0
            currentSelectionIndex = currentSelectionIndex % currentDisplayer.NumberOfCurrentMoves;

        EnterCurrentSelection();
    }

    void SelectCurrentSelection()
    {
        ExitCurrentSelection();
        currentDisplayer.Select(currentSelectionIndex);
    }
    
    void EnterCurrentSelection()
    {
        currentDisplayer.Enter(currentSelectionIndex);
    }

    void ExitCurrentSelection()
    {
        currentDisplayer.Exit(currentSelectionIndex);
    }

    public IEnumerator WaitForCoroutine()
    {
        throw new System.NotImplementedException();
    }

    public BattleMove Value => throw new System.NotImplementedException();
}
