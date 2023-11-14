using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] private float _health;

    private GameObject _target;
    private PoolEnemy _poolEnemy;

    public float Health => _health;

    public GameObject Target => _target;

    public void Initialize(GameObject target, PoolEnemy poolEnemy)
    {
        _target = target;
        _poolEnemy = poolEnemy;
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
        }
    }

    public void ReternToPool()
    {
        gameObject.SetActive(false);
        _poolEnemy.PoolObject(this);
    }
}
