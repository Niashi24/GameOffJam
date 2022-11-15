using System.Collections.Generic;

public interface IBattleUnitPlacer
{
    void PlaceUnits(List<BattleUnit> units);
}

public class NullBattleUnitPlacer : IBattleUnitPlacer
{
    public void PlaceUnits(List<BattleUnit> units) {}
}