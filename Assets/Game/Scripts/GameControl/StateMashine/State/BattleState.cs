using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : GameState
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Player _player;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _player.ResetTurret();
        _spawner.RestSpawner();
    }

    private void Update()
    {
        
    }
}
