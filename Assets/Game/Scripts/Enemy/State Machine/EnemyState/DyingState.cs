using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : State
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
        Enemy.Player.AddMonue(Enemy.Revard);
    }
}
