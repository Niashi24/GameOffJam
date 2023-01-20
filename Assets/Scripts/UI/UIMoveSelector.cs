using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIMoveSelector : MonoBehaviour, IBattleAttackChooser
{
    List<BattleAttack> attacks;

    [SerializeReference]
    IMoveDisplayer _moveDisplayer;

    [SerializeReference]
    ITargetSelector _targetSelector;

    [SerializeField]
    [Required]
    GameObject _descriptionField;

    [SerializeField]
    [Required]
    Keybinds _keybinds;

    BattleUnit currentUnit = null;
    BattleAttack currentAttack = null;
    BattleContext currentContext = null;

    BattleMove selectedMove = null;

    public System.Action<BattleUnit> OnStartCreateAttack;
    //bool is whether it was successful creating attack
    public System.Action<BattleUnit, bool> OnFinishCreateAttack;

    public IEnumerator WaitToChooseAttacks(BattleUnitManager unitManager, BattleContext context)
    {
        _descriptionField.SetActive(true);
        currentContext = context;

        attacks = new();

        var activeUnits = unitManager.ActiveUnits;
        for (int i = 0; i < activeUnits.Count; i++)
        {
            if (!activeUnits[i].CanAttack) continue;
            currentUnit = activeUnits[i];

            OnStartCreateAttack?.Invoke(currentUnit);
            yield return CreateAttack(currentUnit, context);
            
            if (currentAttack == null)
            {
                OnFinishCreateAttack?.Invoke(currentUnit, false);
                //if not first unit, go to previous
                if (i > 0) i -= 2;
                //if first unit, just redo first unit
                else i--;
            }
            else 
            {
                OnFinishCreateAttack?.Invoke(currentUnit, true);
                attacks.Add(currentAttack);
                currentAttack = null;
                currentUnit = null;
            }
        }
        _descriptionField.SetActive(true);
    }

    public IEnumerator WaitToChooseAttacks2(BattleUnitManager unitManager, BattleContext context)
    {
        _descriptionField.SetActive(true);
        currentContext = context;

        attacks = new();

        var activeUnits = unitManager.ActiveUnits;
        for (int i = 0; i < activeUnits.Count; i++)
        {
            if (!activeUnits[i].CanAttack) continue;
            currentUnit = activeUnits[i];

            OnStartCreateAttack?.Invoke(currentUnit);
            yield return CreateAttack(currentUnit, context);
            
            if (currentAttack == null)
            {
                OnFinishCreateAttack?.Invoke(currentUnit, false);
                //if not first unit, go to previous
                if (i > 0) i -= 2;
                //if first unit, just redo first unit
                else i--;
            }
            else 
            {
                OnFinishCreateAttack?.Invoke(currentUnit, true);
                attacks.Add(currentAttack);
                currentAttack = null;
                currentUnit = null;
            }
        }
        _descriptionField.SetActive(true);
    }

    IEnumerator CreateAttack(BattleUnit unit, BattleContext context)
    {
        _moveDisplayer.DisplayMoves(unit.Moves);
        gameObject.SetActive(true);

        while (selectedMove == null)
        {
            //wait until move selection called by MoveSelection
            yield return null;
            //can cancel selection
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(_keybinds.Back))
            {
                gameObject.SetActive(false);
                yield break; //exit attack selection (go back to previous character)
            }
        }

        gameObject.SetActive(false);

        _targetSelector.SetActive(true);
        _targetSelector.DisplayTargets(selectedMove, unit, context);

        while (currentAttack == null)
        {
            yield return null;

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(_keybinds.Back))
            {
                selectedMove = null;
                _targetSelector.SetActive(false);
                yield return CreateAttack(unit, context);
                yield break;
            }
        }

        selectedMove = null;
        _targetSelector.SetActive(false);
        yield break;
    }
    
    public List<BattleAttack> ChooseAttacks(BattleUnitManager unitManager, BattleContext context)
    {
        if (attacks is null) attacks = new();
        return attacks;
    }

    void LoadUnit(BattleUnit unit)
    {
        _moveDisplayer.DisplayMoves(unit.Moves);
    }

    public void SelectMove(BattleMove move)
    {
        if (move.CanBeUsed(currentUnit, currentContext))
            selectedMove = move;
    }

    public void SelectAttack(BattleAttack attack)
    {
        currentAttack = attack;
    }

    private void ResetSelection()
    {
        selectedMove = null;
        currentAttack = null;
    }
}
