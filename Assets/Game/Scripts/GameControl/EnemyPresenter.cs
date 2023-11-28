using System;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    private int _countEnemy;
    private int _deadEnemiesCount;

    public Action OnAllEnemiesDie;
    public Action<int,int> OnEnemyDie;  

    private void Awake()
    {
        _countEnemy = _spawner.GetEnemyCount();
    }

    private void OnEnable()
    {
        _spawner.OnEnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        _spawner.OnEnemyDead -= OnEnemyDead;
    }

    private void OnEnemyDead()
    {
        _deadEnemiesCount++;
        OnEnemyDie.Invoke(_deadEnemiesCount, _countEnemy);

        if (_deadEnemiesCount == _countEnemy)
            OnAllEnemiesDie?.Invoke();
    }

}
