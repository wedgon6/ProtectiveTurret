using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : EnemyState
{
    private void Update()
    {
        if (Enemy.IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Enemy.ReturnToPool();
        Enemy.Player.AddMonuy(Enemy.Revard);
        Enemy.Spawner.OnEnemyDead();
    }
}