using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseGamePanel _loseGamePanel;
    [SerializeField] private EnemySpawner _spawner;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _loseGamePanel.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _spawner.PutEnemyToPool();
        _loseGamePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
