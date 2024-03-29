using SaturnRPG.Battle.Camera;

public class BattleContext
{
    public BattleParty PlayerParty;
    public BattleParty EnemyParty;

    public BattleUnitManager PlayerUnitManager;
    public BattleUnitManager EnemyUnitManager;

    public BattleManager BattleManager;

    public BattleCamera BattleCamera;

    public DescriptionField DescriptionField;
}