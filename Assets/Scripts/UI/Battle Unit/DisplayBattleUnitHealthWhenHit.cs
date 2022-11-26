using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DisplayBattleUnitHealthWhenHit : MonoBehaviour
{
    [SerializeField]
    [Required]
    BattleUnit _battleUnit;

    [SerializeField]
    [Required]
    BattleUnitHealthBar _healthBar;

    [SerializeField]
    float _secondsToDisplay = 2;

    void OnEnable()
    {
        _battleUnit.OnHPChange += DisplayHealth;
    }

    void OnDisable()
    {
        _battleUnit.OnHPChange -= DisplayHealth;
    }

    void DisplayHealth(float hp)
    {
        StartCoroutine(DisplayHealthCoroutine());
    }

    IEnumerator DisplayHealthCoroutine()
    {
        _healthBar.Push(true);
        yield return new WaitForSeconds(_secondsToDisplay);
        _healthBar.Pop();
    }
}
