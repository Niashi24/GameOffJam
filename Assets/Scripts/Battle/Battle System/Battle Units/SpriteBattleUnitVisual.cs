using Sirenix.OdinInspector;
using UnityEngine;

public class SpriteBattleUnitVisual : BattleUnitVisual, IBounds
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

    public Bounds Bounds2D => _spriteRenderer.bounds;
}