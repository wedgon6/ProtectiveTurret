using UnityEngine;

public class BattleState : GameState
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private BattleStatePanel _battleStatePanel;
    [SerializeField] private SaveAndLoadSytem _saveAndLoadSytem;

    public override void Enter(Player player)
    {
        _battleStatePanel.gameObject.SetActive(true);
        base.Enter(player);
        _player.ResetTurret();
        _player.SetMovmentMode(true);
        _spawner.RestSpawner();
    }

    public override void Exit()
    {
        _saveAndLoadSytem.SetSaveData();
        _battleStatePanel.gameObject.SetActive(false);
        base.Exit();
    }
}
