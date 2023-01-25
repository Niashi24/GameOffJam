using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BattleUnitVisual : MonoBehaviour
{
    [SerializeField]
    [Required]
    BattleUnit _battleUnit;

    public virtual void OnEnable()
    {
        _battleUnit.OnSetPartyMember += SetVisual;
    }

    public virtual void OnDisable()
    {
        _battleUnit.OnSetPartyMember -= SetVisual;
    }

    public abstract void SetVisual(BattleUnit unit, BasePartyMember partyMember);

    public abstract void SetActive(bool isActive);
}
