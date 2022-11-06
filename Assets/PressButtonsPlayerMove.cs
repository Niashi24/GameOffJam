using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PressButtonsPlayerMove : PlayerMoveComponent
{
    [SerializeField]
    PressButtonsMoveButtonScript _buttonPrefab;

    ObjectPool<PressButtonsMoveButtonScript> buttonPool;

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

    public override IEnumerator PlayAttack(BattleContext context, PlayerMove playerMove)
    {
        throw new System.NotImplementedException();
    }

    public override float getAttackScore()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PlayEffect(BattleContext context, PlayerMove playerMove, float attackScore)
    {
        throw new System.NotImplementedException();
    }
}
