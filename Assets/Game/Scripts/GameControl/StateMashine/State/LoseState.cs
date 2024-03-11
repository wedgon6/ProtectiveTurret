using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseGamePanel _loseGamePanel;
    [SerializeField] private EnemySpawner _spawner;

    public override void Enter(Player player)
    {
        _loseGamePanel.gameObject.SetActive(true);
        _spawner.PutEnemyToPool();
        base.Enter(player);
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        _loseGamePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        
    }
}
