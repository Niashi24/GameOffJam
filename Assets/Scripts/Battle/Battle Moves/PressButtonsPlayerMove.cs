using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class PressButtonsPlayerMove : BattleMoveComponent
{
    [SerializeField]
    PressButtonsMoveButtonScript _buttonPrefab;

    [SerializeField]
    Transform _buttonParent;

    [SerializeField]
    ValueReference<int> _numButtons = new ValueReference<int>(3);
    
    [SerializeField]
    float _secondsPerButton = 2;

    ObjectPool<PressButtonsMoveButtonScript> buttonPool;

    [ShowInInspector, ReadOnly]
    int _buttonsPressed;

    void Start()
    {
        buttonPool = new ObjectPool<PressButtonsMoveButtonScript>
        (
            () => Instantiate(
                    _buttonPrefab, _buttonParent.position, Quaternion.identity, _buttonParent
                ),
            (x) => x.gameObject.SetActive(true),
            (x) => x.gameObject.SetActive(false),
            (x) => Destroy(x.gameObject)
        );
    }

    public override IEnumerator PlayAttack(BattleContext context, BattleAttack playerMove)
    {
        int numButtons = _numButtons.Value;
        // List<PressButtonsMoveButtonScript> buttons = new(numButtons);
        PressButtonsMoveButtonScript[] buttons = new PressButtonsMoveButtonScript[numButtons];
        _buttonsPressed = 0;
        for (int i = 0; i < numButtons; i++)
        {
            buttons[i] = buttonPool.Get();
            buttons[i].OnFirstPress += () => _buttonsPressed++;
            const float w = 60;

            buttons[i].transform.position = buttons[i].transform.position.With(
                x: _buttonParent.position.x + (i+1)/2 * w * Mathf.Pow(-1, i+1)
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
        playerMove.Target.DealDamage(DamageCalculator.CalculateDamage(playerMove, attackScore));

        yield break;
    }
}
