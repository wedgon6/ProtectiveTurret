using UnityEngine;

public class BattleState : GameState
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private WaveProgressBar _progressBar;

    public override void Enter(Player player)
    {
        _progressBar.gameObject.SetActive(true);
        base.Enter(player);
        _player.ResetTurret();
        _spawner.RestSpawner();
    }

    public override void Exit()
    {
        base.Exit();
        _progressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
