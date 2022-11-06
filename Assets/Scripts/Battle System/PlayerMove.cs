using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle System/Player Move")]
public class PlayerMove : ScriptableObject
{
    [SerializeField]
    PlayerMoveComponent _movePrefab;
}
