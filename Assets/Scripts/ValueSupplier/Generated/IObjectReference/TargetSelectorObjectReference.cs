using LS.Utilities;
using UnityEngine;

[System.Serializable]
public class TargetSelectorObjectReference : ITargetSelector
{
    [SerializeField]
    ObjectReference<ITargetSelector> _targetSelector;

    public void DisplayTargets(BattleMove move, BattleUnit user, BattleContext context) => _targetSelector.Value.DisplayTargets(move, user, context);
}