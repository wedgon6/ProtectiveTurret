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
        Enemy.Player.AddMoney(Enemy.Revard,Enemy.CountScore);
        Enemy.Spawner.OnEnemyDead();
    }
}