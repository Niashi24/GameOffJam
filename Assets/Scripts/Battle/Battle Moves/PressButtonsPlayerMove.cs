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
                    _buttonPrefab, transform.position, Quaternion.identity, transform
                ),
            (x) => x.gameObject.SetActive(true),
            (x) => x.gameObject.SetActive(false),
            (x) => Destroy(x.gameObject)
        );
    }

    public override IEnumerator PlayAttack(BattleContext context, BattleAttack playerMove)
    {
        List<PressButtonsMoveButtonScript> buttons = new();
        _buttonsPressed = 0;
        int numButtons = _numButtons.Value;
        for (int i = 0; i < numButtons; i++)
        {
            buttons[i] = buttonPool.Get();
            buttons[i].OnFirstPress += () => _buttonsPressed++;
        }

        yield return WaitUntilMoveFinished(numButtons);

        buttons.ForEach((x) => {
            x.ResetButton();
            buttonPool.Release(x);
        });
    }

    //returns true either when time has run out or when 
    private IEnumerator WaitUntilMoveFinished(int numButtons)
    {
        CoroutineHandle waitForSeconds = this.RunCoroutine(WaitSeconds(numButtons));
        while (!waitForSeconds.IsDone || _buttonsPressed < numButtons)
            yield return null;
    }

    private IEnumerator WaitSeconds(int numButtons)
    {
        yield return new WaitForSeconds(numButtons * _secondsPerButton);
    }

    public override float getAttackScore()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PlayEffect(BattleContext context, BattleAttack playerMove, float attackScore)
    {
        throw new System.NotImplementedException();
    }
}
