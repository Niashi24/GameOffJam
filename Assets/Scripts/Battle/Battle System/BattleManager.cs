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
    BattleUnitManager _playerUnitManager;
    
    [SerializeField]
    [Required]
    BattleUnitManager _enemyUnitManager;

    [ShowInInspector, ReadOnly]
    BattleContext _context = new();

    int _turnNumber;

    [ShowInInspector, ReadOnly]
    public int TurnNumber => _turnNumber;

    [SerializeReference]
    IBattleAttackChooser _playerAttackChooser;

    [SerializeReference]
    IBattleAttackChooser _enemyAttackChooser;

    BattleState _battleState = BattleState.BattleStart;
    public BattleState BattleState => _battleState;

    public static ActionCoroutine<BattleState> OnBattleStateChange = new();

    //returns true if player won, false if not
    //make sure to subscribe methods AFTER starting the battle
    public Action<bool> OnBattleFinish;

    public Action<int> OnTurnFinish;

    void Start()
    {
        OnBattleStateChange.Subscribe(LogState);
    }

    private IEnumerator LogState(BattleState state)
    {
        Debug.Log($"Changed state to {state}.");
        yield break;
    }

    [Button]
    public void StartBattle(BattleParty playerParty, BattleParty enemyParty)
    {
        _context.PlayerParty = playerParty;
        _context.EnemyParty = enemyParty;
        _context.PlayerUnitManager = _playerUnitManager;
        _context.EnemyUnitManager = _enemyUnitManager;

        OnBattleFinish = null;

        _battleState = BattleState.BattleStart;
        StartCoroutine(BattleCoroutine());
    }

    void InitializePlayerUnits(BattleParty playerParty)
    {
        _playerUnitManager.InitializeBattleUnits(playerParty);
    }
    
    private void InitializeEnemyUnits(BattleParty enemyParty)
    {
        _enemyUnitManager.InitializeBattleUnits(enemyParty);
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

            yield return _playerAttackChooser.WaitToChooseAttacks(_playerUnitManager, _context);
            List<BattleAttack> playerAttacks = _playerAttackChooser.ChooseAttacks(_playerUnitManager, _context);

            foreach (var attack in playerAttacks.OrderBy(x => x.User.BaseMember.BattleStats.Quickness))
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
            yield return _enemyAttackChooser.WaitToChooseAttacks(_enemyUnitManager, _context);
            List<BattleAttack> enemyAttacks = _enemyAttackChooser.ChooseAttacks(_enemyUnitManager, _context);

            foreach (var attack in enemyAttacks.OrderBy(x => x.User.BaseMember.BattleStats.Quickness))
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

            _turnNumber++;
            OnTurnFinish?.Invoke(_turnNumber);
        }
    }

    IEnumerator EndBattle(bool playersWon)
    {
        _battleState = BattleState.BattleEnd;

        yield return OnBattleStateChange.Invoke(_battleState);


    }

    bool AllEnemiesDown()
    {
        if (_enemyUnitManager.ActiveUnits.Count == 0) return true;
        foreach (var enemy in _enemyUnitManager.ActiveUnits)
        {
            if (enemy.HP > 0) return false;
        }

        return true;
    }

    bool AllPlayersDown()
    {
        if (_playerUnitManager.ActiveUnits.Count == 0) return true;
        foreach (var player in _playerUnitManager.ActiveUnits)
        {
            if (player.InitialHP > 0) return false;
        }

        return true;
    }
}
