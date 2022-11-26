using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class UITargetSelector : MonoBehaviour, ITargetSelector
{
    [SerializeField]
    List<UIBattleUnitTarget> _targets;

    [SerializeField]
    [Required]
    UIMoveSelector _moveSelector;

    [SerializeField]
    [Required]
    Keybinds _keybinds;

    [ShowInInspector, ReadOnly]
    public List<UIBattleUnitTarget> ActiveTargets {get; private set;} = new();

    [ShowInInspector, ReadOnly] //Active Targets sorted by X
    public List<UIBattleUnitTarget> ActiveTargetsX {get; private set;}
    [ShowInInspector, ReadOnly] //Active Targets sorted by Y
    public List<UIBattleUnitTarget> ActiveTargetsY {get; private set;}
    //stored when DisplayTargets is called so can createe Attack late
    BattleMove currentMove = null;
    BattleUnit currentUser = null;

    int selectedTargetIndexX = 0;
    int selectedTargetIndexY = 0;

    public System.Action<List<UIBattleUnitTarget>> OnDisplayTargets;

    public void DisplayTargets(BattleMove move, BattleUnit user, BattleContext context)
    {
        _targets.ForEach(x => x.gameObject.SetActive(false));
        ActiveTargets.Clear();
        selectedTargetIndexX = 0;
        selectedTargetIndexY = 0;

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

            _targets[targetIndex].SetTarget(target, this);
            _targets[targetIndex].gameObject.SetActive(true);
            ActiveTargets.Add(_targets[targetIndex]);
            targetIndex++;
        }

        ActiveTargetsX = ActiveTargets.OrderBy(x => x.transform.position.x).ToList();
        ActiveTargetsY = ActiveTargets.OrderBy(x => x.transform.position.y).ToList();
        if (ActiveTargetsX.Count != 0)
            ActiveTargetsX[selectedTargetIndexX].EnableOutline();

        OnDisplayTargets?.Invoke(ActiveTargets);
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

        ActiveTargetsX[selectedTargetIndexX].DisableOutline();

        currentMove = null;
        currentUser = null;
        ActiveTargets.Clear();

        _moveSelector.SelectAttack(attack);
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    void Update()
    {
        if (Input.GetKeyDown(_keybinds.Up))
            MoveUD(1);
        if (Input.GetKeyDown(_keybinds.Down))
            MoveUD(-1);

        if (Input.GetKeyDown(_keybinds.Right))
            MoveLR(1);
        if (Input.GetKeyDown(_keybinds.Left))
            MoveLR(-1);

        //NOTE! This method is triggered right before DisplayTargets instead of sometime after 
        //(race conditions moment)
        if (Input.GetKeyDown(_keybinds.Confirm))
            SelectCurrent();
    }

    void MoveUD(int y)
    {
        if (ActiveTargets.Count == 0) return;

        ActiveTargetsY[selectedTargetIndexY].DisableOutline();

        selectedTargetIndexY += y;

        while (selectedTargetIndexY < 0)
            selectedTargetIndexY += ActiveTargetsY.Count;
        selectedTargetIndexY = selectedTargetIndexY % ActiveTargetsY.Count;

        ActiveTargetsY[selectedTargetIndexY].EnableOutline();
        selectedTargetIndexX = ActiveTargetsX.IndexOf(ActiveTargetsY[selectedTargetIndexY]);
    }

    void MoveLR(int x)
    {
        if (ActiveTargets.Count == 0) return;

        ActiveTargetsX[selectedTargetIndexX].DisableOutline();

        selectedTargetIndexX += x;

        while (selectedTargetIndexX < 0)
            selectedTargetIndexX += ActiveTargetsX.Count;
        selectedTargetIndexX = selectedTargetIndexX % ActiveTargetsX.Count;

        ActiveTargetsX[selectedTargetIndexX].EnableOutline();
        selectedTargetIndexY = ActiveTargetsY.IndexOf(ActiveTargetsX[selectedTargetIndexX]);
    }

    //NOTE! This method is triggered right before DisplayTargets instead of sometime after (race conditions moment)
    void SelectCurrent()
    {
        if (ActiveTargets.Count == 0) return;

        ActiveTargetsX[selectedTargetIndexX].Select();
    }
}
