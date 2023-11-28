using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private int _revard;

    private ReadLine _target;
    private PoolEnemy _poolEnemy;
    private float _health;
    private Player _player;
    private bool _isDead;
    private EnemySpawner _spawner;

    public float Health => _health;
    public bool IsDead => _isDead;
    public ReadLine Target => _target;
    public int Revard => _revard;
    public Player Player => _player;

    public void Initialize(ReadLine target, PoolEnemy poolEnemy, Player player, EnemySpawner spawner)
    {
        _target = target;
        _poolEnemy = poolEnemy;
        _health = _maxHealth;
        _player = player;
        _spawner = spawner;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_health < 0)
            return;

        _health -= damage;

        if (_health < 0)
        {
            _health = 0;
            _spawner.OnEnemyDead();
            _isDead = true;
        }
    }

    public void ResetState()
    {
        _stateMachine.ResetStete();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _poolEnemy.PoolObject(this);
        _health = _maxHealth;
        _isDead = false;
    }
}
