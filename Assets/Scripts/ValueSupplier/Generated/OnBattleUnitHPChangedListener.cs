using UnityEngine;
using UnityEngine.Events;

public class OnBattleUnitHPChangedListener : MonoBehaviour
{
    [SerializeField]
    UnityAction<float> OnBattleUnitHPChanged;

    [SerializeField]
    BattleUnit _unit;

    public void SetUnit(BattleUnit unit)
    {
        
    }
}