using UnityEngine;

public class BattleState : GameState
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private MovementPlayer _movementPlayer;
    [SerializeField] private BattleStatePanel _battleStatePanel;
    [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;

    public override void Enter()
    {
        _battleStatePanel.gameObject.SetActive(true);
        base.Enter();
        _player.ResetTurret();
        _spawner.RestSpawner();
    }

    public override void Exit()
    {
        _saveAndLoadSytem.SetSaveData();
        _battleStatePanel.gameObject.SetActive(false);
        _movementPlayer.SetModeMovmen(false);
        base.Exit();
    }
}