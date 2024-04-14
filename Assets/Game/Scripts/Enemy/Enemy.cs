using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private int _revard;
    [SerializeField] private int _countScore;
    [SerializeField] private GameObject _backlight;
    [SerializeField] private GameObject _deadParticle;
    [SerializeField] private TypeEnemy _typeEnemy;

    private RedLine _target;
    private float _health;
    private Player _player;
    private bool _isDead;
    private EnemySpawner _spawner;

    public event Action<IPoolObject> PoolReturned;
    public RedLine Target => _target;
    public Player Player => _player;
    public EnemySpawner Spawner => _spawner;
    public float Health => _health;
    public int Revard => _revard;
    public int CountScore => _countScore;
    public bool IsDead => _isDead;
    public string TypeEnemy => _typeEnemy.ToString();

    public void Initialize(RedLine target, Player player, EnemySpawner spawner)
    {
        _target = target;
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
            _isDead = true;
        }
    }

    public void ResetState() => _stateMachine.ResetStete();

    public void GetCrosshairs() => _backlight.SetActive(true);

    public void Dead()
    {
        Instantiate(_deadParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        _backlight.SetActive(false);
        PoolReturned?.Invoke(this);
        _health = _maxHealth;
        _isDead = false;
    }
}