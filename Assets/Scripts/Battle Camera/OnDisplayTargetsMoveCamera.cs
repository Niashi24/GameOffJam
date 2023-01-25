using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaturnRPG.Battle.Camera
{
    public class OnDisplayTargetsMoveCamera : MonoBehaviour
    {
        [SerializeField]
        [Required]
        BattleCamera _battleCamera;

        [SerializeField]
        [Required]
        UITargetSelector _UITargetSelector;

        void OnEnable()
        {
            _UITargetSelector.OnDisplayTargetableUnits += OnDisplayTargets;
        }

        void OnDisable()
        {
            _UITargetSelector.OnDisplayTargetableUnits -= OnDisplayTargets;
        }

        void OnDisplayTargets(List<BattleUnit> battleUnitTargets)
        {
            Vector3 averagePosition = Vector3.zero;
            foreach (var unit in battleUnitTargets)
                averagePosition += unit.transform.position;
            _battleCamera.SetTargetWorldPosition(averagePosition / battleUnitTargets.Count);
        }
    }
}
