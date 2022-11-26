using UnityEngine;
using UnityEngine.Events;

public class OnBattleUnitHPChangedListener : MonoBehaviour
{
    [SerializeField]
    UnityEvent<float> OnBattleUnitHPChanged;

    [SerializeField]
    BattleUnit _unit;
    
    
}