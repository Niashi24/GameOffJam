using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaturnRPG.Battle.Camera
{
    public class OnStartMoveSelectionMoveCamera : MonoBehaviour
    {
        [SerializeField]
        [Required]
        BattleCamera _battleCamera;

        [SerializeField]
        [Required]
        UIMoveSelector _UIMoveSelector;

        void OnEnable()
        {
            _UIMoveSelector.OnStartCreateAttack += OnStartCreateAttack;
        }

        void OnDisable()
        {
            _UIMoveSelector.OnStartCreateAttack -= OnStartCreateAttack;
        }

        private void OnStartCreateAttack(BattleUnit battleUnit)
        {
            _battleCamera.SetTargetTransform(battleUnit.transform);
        }
    }
}
