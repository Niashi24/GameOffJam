using Sirenix.OdinInspector;
using UnityEngine;

public class SpriteBattleUnitVisual : BattleUnitVisual
{
    [SerializeField]
    [Required]
    SpriteRenderer _spriteRenderer;

    public override void SetVisual(BattleUnit unit, BasePartyMember partyMember)
    {
        _spriteRenderer.sprite = partyMember.BattleSprite;
    }

    public override void SetActive(bool isActive)
    {
        _spriteRenderer.enabled = isActive;
    }
}