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
        Enemy.Player.AddMoney(Enemy.Revard);
        Enemy.Player.AddScore(Enemy.CountScore);
        Enemy.Spawner.OnEnemyDead();
    }
}