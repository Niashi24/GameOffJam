public interface ITargetSelector
{
    void DisplayTargets(BattleMove move, BattleUnit user, BattleContext context);

    void SetActive(bool active);
}