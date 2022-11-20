using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveSelector : MonoBehaviour, IBattleAttackChooser
{
    List<BattleAttack> attacks;

    [SerializeReference]
    IMoveDisplayer _moveDisplayer;

    [SerializeReference]
    ITargetSelector _targetSelector;

    [SerializeField]
    GameObject _descriptionField;

    BattleAttack currentAttack = null;

    BattleMove selectedMove = null;

    public IEnumerator WaitToChooseAttacks(BattleUnitManager unitManager, BattleContext context)
    {
        _descriptionField.SetActive(true);

        attacks = new();

        var activeUnits = unitManager.ActiveUnits;
        for (int i = 0; i < activeUnits.Count; i++)
        {
            yield return CreateAttack(activeUnits[i], context);
            
            if (currentAttack == null)
            {
                //if not first unit, go to previous
                if (i > 0) i -= 2;
                //if first unit, just redo first unit
                else i--;
            }
            else 
            {
                attacks.Add(currentAttack);
                currentAttack = null;
            }
        }
        _descriptionField.SetActive(true);
    }

    IEnumerator CreateAttack(BattleUnit unit, BattleContext context)
    {
        _moveDisplayer.DisplayMoves(unit.BaseMember.Moves);
        gameObject.SetActive(true);

        while (selectedMove == null)
        {
            //wait until move selection
            yield return null;
            //can cancel selection
            if (Input.GetMouseButtonDown(1))
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

            if (Input.GetMouseButtonDown(1))
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
        _moveDisplayer.DisplayMoves(unit.BaseMember.Moves);
    }

    public void SelectMove(BattleMove move)
    {
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
