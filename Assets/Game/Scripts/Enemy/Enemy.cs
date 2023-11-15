using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private EnemyStateMachine _stateMachine;

    private GameObject _target;
    private PoolEnemy _poolEnemy;
    private float _health;
    private bool _isDead;

    public float Health => _health;
    public bool IsDead => _isDead;
    public GameObject Target => _target;

    public void Initialize(GameObject target, PoolEnemy poolEnemy)
    {
        _target = target;
        _poolEnemy = poolEnemy;
        _health = _maxHealth;
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
