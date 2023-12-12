using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RedLine _target;
    [SerializeField] private List<EnemyWave> _waves;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PoolEnemy _poolEnemy;
    [SerializeField] private Player _player;

    private EnemyWave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private Coroutine _corontine;
    private List<IPoolObject> _createdEnemies = new List<IPoolObject>();

    public Action OnEnemyDead;
    public Action OnSpawnerReset;

    public void EnemyDead()
    {
        OnEnemyDead?.Invoke();
    }

    public void PutEnemyToPool()
    {
        if (_createdEnemies.Count > 0)
        {
            foreach (var enemy in _createdEnemies)
            {
                enemy.ReturnToPool();
            }
        }

        _currentWave = null;
    }

    public void RestSpawner()
    {
        PutEnemyToPool();

        _timeAfterLastSpawn = 0;
        _currentWaveNumber = 0;
        _spawned = 0;
        SetWave(_currentWaveNumber);
        OnSpawnerReset?.Invoke();
    }

    public int GetEnemyCount()
    {
        int enemyCount = 0;

        for (int i = 0; i < _waves.Count; i++)
        {
            enemyCount += _waves[i].Count;
        }

        return enemyCount;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InitializeEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                CorountineStart(StartNextWave());

            _currentWave = null;
        }
    }

    private void InitializeEnemy()
    {
        int currentSpawnPont = UnityEngine.Random.Range(0, _spawnPoint.Length);
        Enemy enemy;

        if (_poolEnemy.TryPoolObject(out IPoolObject enemyPool))
        {
            enemy = enemyPool as Enemy;
            enemy.transform.position = _spawnPoint[currentSpawnPont].position;
            enemy.gameObject.SetActive(true);
            enemy.ResetState();
        }
        else
        {
            enemy = Instantiate(_currentWave.Template, _spawnPoint[currentSpawnPont].position, _spawnPoint[currentSpawnPont].rotation,
            _spawnPoint[currentSpawnPont]).GetComponent<Enemy>();
            enemy.Initialize(_target, _poolEnemy, _player, this);
            _createdEnemies.Add(enemy);
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private IEnumerator StartNextWave()
    {
        int waitTime = 1;
        yield return new WaitForSeconds(waitTime);
        NextWave();
    }

    private void CorountineStart(IEnumerator corontine)
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(corontine);
    }
}
