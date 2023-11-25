using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ReadLine _target;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private PoolEnemy _poolEnemy;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Awake()
    {
        SetWave(_currentWaveNumber);
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
            {
                StartCoroutine(StartNextWave());
            }

            _currentWave = null;
        }
    }

    private void InitializeEnemy()
    {
        int currentSpawnPont = Random.Range(0, _spawnPoint.Length);
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
            enemy.Initialize(_target, _poolEnemy, _player);
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
        int waitTime = 5;
        yield return new WaitForSeconds(waitTime);
        NextWave();
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
}
