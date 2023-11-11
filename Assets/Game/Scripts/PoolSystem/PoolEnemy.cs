using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolEnemy : SpawnableObject
{
    [SerializeField] private float _health;

    private Transform _transform;
    private Spawner _spawner;
    private GameObject _target;

    public float Health => _health;
    public GameObject Target => _target;

    private void Awake()
    {
        _transform = transform;
    }

    internal override SpawnableObject Initialize<T>(T param, Spawner spawner)
    {
        _spawner = spawner;
        _target = param as GameObject;
        return this;
    }

    public override SpawnableObject Pull(Vector3 position)
    {
        _transform.position = position;
        SetActive(true);
        return this;
    }

    public override void Push()
    {
        _spawner.Push(this);
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
}
