using System.Collections.Generic;

public interface IMoveDisplayer //: ICoroutineValue<BattleMove>
{
    void DisplayMoves(List<BattleMove> moves);
}
