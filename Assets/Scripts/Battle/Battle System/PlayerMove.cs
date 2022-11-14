using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Battle Move")]
public class BattleMove : ScriptableObject
{
    [SerializeField]
    BattleMoveComponent _movePrefab;
}