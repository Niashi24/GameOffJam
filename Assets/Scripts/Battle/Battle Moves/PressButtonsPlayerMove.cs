using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class PressButtonsPlayerMove : BattleMoveComponent
{
    [SerializeField]
    [Required]
    PressButtonsMoveButtonScript _buttonPrefab;

    [SerializeField]
    [Required]
    Transform _buttonParent;

    [SerializeField]
    ValueReference<int> _numButtons = new ValueReference<int>(3);
    
    [SerializeField]
    float _secondsPerButton = 2;

    ObjectPool<PressButtonsMoveButtonScript> buttonPool;

    [ShowInInspector, ReadOnly]
    int _buttonsPressed;

    void Awake()
    {
        buttonPool = _buttonPrefab.CreateMonoPool(position: _buttonParent.position, parent: _buttonParent);
    }

    public override IEnumerator PlayAttack(BattleContext context, BattleAttack battleAttack)
    {
        context.DescriptionField.SetText(battleAttack.MoveBase.MoveName);
        context.DescriptionField.SetActive(true);
        yield return new WaitForSeconds(1);
        context.DescriptionField.SetActive(false);
        
        context.BattleCamera.SetTargetTransform(battleAttack.Target.transform);
        yield return new WaitForSeconds(1);

        int numButtons = _numButtons.Value;
        // List<PressButtonsMoveButtonScript> buttons = new(numButtons);
        PressButtonsMoveButtonScript[] buttons = new PressButtonsMoveButtonScript[numButtons];
        _buttonsPressed = 0;
        for (int i = 0; i < numButtons; i++)
        {
            buttons[i] = buttonPool.Get();
            buttons[i].OnFirstPress += () => _buttonsPressed++;
            const float w = 60;

            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(
                (i+1)/2 * w * Mathf.Pow(-1, i+1), 0, 0
            );
        }

        yield return WaitUntilMoveFinished(numButtons);

        foreach(var button in buttons)
        {
            button.ResetButton();
            buttonPool.Release(button);
        }
    }

    //returns true either when time has run out or when 
    private IEnumerator WaitUntilMoveFinished(int numButtons)
    {
        CoroutineHandle waitForSeconds = this.RunCoroutine(WaitSeconds(numButtons));
        while (!waitForSeconds.IsDone && _buttonsPressed < numButtons)
            yield return null;
    }

    private IEnumerator WaitSeconds(int numButtons)
    {
        yield return new WaitForSeconds(numButtons * _secondsPerButton);
    }


    public override float GetAttackScore()
    {
        return ((float)_buttonsPressed)/_numButtons.Value;
    }

    public override IEnumerator PlayEffect(BattleContext context, BattleAttack playerMove, float attackScore)
    {
        //TODO: play damage animation
        playerMove.Target.DealDamage(playerMove, attackScore);

        yield return new WaitForSeconds(2);
    }

    public override List<BattleUnit> GetTargetableUnits(BattleUnit user, BattleContext context)
    {
        List<BattleUnit> targetableUnits = new();

        if (context.PlayerUnitManager.ActiveUnits.Contains(user))
            targetableUnits.Add(context.EnemyUnitManager.All);
        else
            targetableUnits.Add(context.PlayerUnitManager.All);
        return targetableUnits;
    }

    public override bool CanBeUsed(BattleUnit user, BattleContext context)
    {
        return GetTargetableUnits(user, context).Count > 0;
    }
}
