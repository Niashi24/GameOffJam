using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    [Required]
    PlayerUnitManager _playerUnitManager;

    [SerializeField]
    List<EnemyUnit> _enemyUnits;
    
    [SerializeField]
    [Required]
    EnemyUnitManager _enemyUnitManager;

    [ShowInInspector, ReadOnly]
    BattleContext _context = new();

    int _turnNumber;

    [ShowInInspector, ReadOnly]
    public int TurnNumber => _turnNumber;

    [SerializeReference]
    IPlayerAttackChooser _playerAttackChooser;

    [SerializeReference]
    IEnemyAttackChooser _enemyAttackChooser;

    BattleState _battleState = BattleState.BattleStart;
    public BattleState BattleState => _battleState;

    public static ActionCoroutine<BattleState> OnBattleStateChange;

    //returns true if player won, false if not
    //make sure to subscribe methods AFTER starting the battle
    public Action<bool> OnBattleFinish;

    public void StartBattle(PlayerParty playerParty, EnemyParty enemyParty)
    {
        _context.PlayerParty = playerParty;
        _context.EnemyParty = enemyParty;

        OnBattleFinish = null;

        _battleState = BattleState.BattleStart;
        StartCoroutine(BattleCoroutine());
    }

    void InitializePlayerUnits(PlayerParty playerParty)
    {
        _playerUnitManager.InitializePlayerUnits(playerParty);
    }
    
    private void InitializeEnemyUnits(EnemyParty enemyParty)
    {
        _enemyUnitManager.InitializeEnemyUnits(enemyParty);
    }

    IEnumerator BattleCoroutine()
    {
        yield return OnBattleStateChange.Invoke(BattleState.BattleStart);

        InitializePlayerUnits(_context.PlayerParty);
        InitializeEnemyUnits(_context.EnemyParty);

        while (!(AllPlayersDown() || AllEnemiesDown()))
        {
            _battleState = BattleState.PlayerAttack;
            yield return OnBattleStateChange.Invoke(_battleState);

            _playerAttackChooser.Wait();
            List<PlayerAttack> playerAttacks = _playerAttackChooser.ChooseAttacks(_playerUnitManager);

            foreach (var attack in playerAttacks.OrderBy(x => x.User.BasePlayer.GetStats().Quickness))
            {
                yield return attack.PlayAttack(_context);

                if (AllPlayersDown())
                {
                    yield return EndBattle(false);
                    yield break;
                }
                if (AllEnemiesDown())
                {
                    yield return EndBattle(true);
                    yield break;
                }
            }

            _battleState = BattleState.EnemyAttack;
            yield return OnBattleStateChange.Invoke(_battleState);

            //Choose Enemy Attack

            List<EnemyAttack> enemyAttacks = _enemyAttackChooser.ChooseAttacks(_enemyUnitManager);

            foreach (var attack in enemyAttacks.OrderBy(x => x.User.BaseEnemy.GetStats().Quickness))
            {
                yield return attack.PlayAttack(_context);

                if (AllPlayersDown())
                {
                    yield return EndBattle(false);
                    yield break;
                }
                if (AllEnemiesDown())
                {
                    yield return EndBattle(true);
                    yield break;
                }
            }

        }
    }

    IEnumerator EndBattle(bool playersWon)
    {
        _battleState = BattleState.BattleEnd;

        yield return OnBattleStateChange.Invoke(_battleState);


    }

    bool AllEnemiesDown()
    {
        foreach (var enemy in _enemyUnits)
        {
            if (enemy.HP > 0) return false;
        }

        return true;
    }

    bool AllPlayersDown()
    {
        foreach (var player in _playerUnitManager.PlayerUnits)
        {
            if (player.HP > 0) return false;
        }

        return true;
    }
}
