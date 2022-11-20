using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UITargetSelector : MonoBehaviour, ITargetSelector
{
    [SerializeField]
    List<UIBattleUnitTarget> _targets;

    [SerializeField]
    [Required]
    UIMoveSelector _moveSelector;

    BattleMove currentMove = null;
    BattleUnit currentUser = null;

    public void DisplayTargets(BattleMove move, BattleUnit user, BattleContext context)
    {
        _targets.ForEach(x => x.gameObject.SetActive(false));

        List<BattleUnit> targets = move.GetTargetableUnits(user, context);
        currentMove = move;
        currentUser = user;

        int targetIndex = 0;
        foreach (var target in targets)
        {
            if (targetIndex >= _targets.Count)
            {
                Debug.LogError("Error! Out of targets.", this);
                break;
            }

            _targets[targetIndex].SetTarget(target);
            _targets[targetIndex].gameObject.SetActive(true);
            targetIndex++;
        }
    }

    //called by target
    public void SelectTarget(BattleUnit target)
    {
        BattleAttack attack = new BattleAttack()
        {
            MoveBase = currentMove,
            User = currentUser,
            Target = target
        };

        currentMove = null;
        currentUser = null;

        _moveSelector.SelectAttack(attack);
    }

    public void SetActive(bool active) => gameObject.SetActive(active);
}
