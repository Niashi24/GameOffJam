using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaturnRPG.Battle.Camera
{
    public class OnStartAttackMoveCamera : MonoBehaviour
    {
        [SerializeField]
        [Required]
        BattleCamera _battleCamera;

        [SerializeField]
        [Required]
        BattleManager _battleManager;

        void OnEnable()
        {
            _battleManager.OnBeforeAttack += OnBeforeAttack;
        }

        void OnDisable()
        {
            _battleManager.OnBeforeAttack -= OnBeforeAttack;
        }

        void OnBeforeAttack(BattleAttack battleAttack)
        {
            _battleCamera.SetTargetTransform(battleAttack.User.transform);
        }
    }
}
