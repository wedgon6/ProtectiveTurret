using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int EasyWaveIndex = 3;
    private const int MidWaveIndex = 7;
    private const int HardWaveIndex = 11;
    private const int WaveLenght = 5;

    [SerializeField] private RedLine _target;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private EnemiesList _enemiesPrefab;
    [SerializeField] private float _delay;

    [SerializeField] private PoolEnemy _poolStandartEnemy;
    [SerializeField] private PoolEnemy _poolFastEnemy;
    [SerializeField] private PoolEnemy _poolBigEnemy;

    private EnemyWave _currentWave = null;
    private int _currentWaveNumber = 0;
    private int _countWaves;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private Coroutine _corontine;
    private List<IPoolObject> _createdEnemies = new List<IPoolObject>();
    private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

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
        _enemyWaves.Clear();

        _timeAfterLastSpawn = 0;
        _currentWaveNumber = 0;
        _spawned = 0;
        SetWaveComplexity();
    }

    public int GetEnemyCount()
    {
        int enemyCount = 0;

        for (int i = 0; i < _enemyWaves.Count; i++)
        {
            enemyCount += _enemyWaves[i].Template.Count;
        }

        return enemyCount;
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            Debug.Log("вышел из апдейта");
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _delay)
        {
            InitializeEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Template.Count <= _spawned)
        {
            Debug.Log(_spawned);
            Debug.Log(_currentWave.Template.Count);
            if (_enemyWaves.Count > _currentWaveNumber + 1)
                CorountineStart(StartNextWave());

            _currentWave = null;
        }
    }

    private void InitializeEnemy()
    {

        int currentSpawnPont = UnityEngine.Random.Range(0, _spawnPoint.Length);
        Enemy enemy = _currentWave.GetNextEnemy();

        if (enemy == null)
            return;

        if (TyrFindEnemy(enemy, out Enemy poolEnemy))
        {
            enemy = poolEnemy;
            enemy.transform.position = _spawnPoint[currentSpawnPont].position;
            enemy.gameObject.SetActive(true);
            enemy.ResetState();
        }
        else
        {
            enemy = Instantiate(enemy, _spawnPoint[currentSpawnPont].position, _spawnPoint[currentSpawnPont].rotation,
            _spawnPoint[currentSpawnPont]).GetComponent<Enemy>();
            InitializeEnemy(enemy);
            _createdEnemies.Add(enemy);
        }
    }

    private void InitializeEnemy(Enemy enemy)
    {
        if (enemy.TryGetComponent(out StandartEnemy standartEnemy))
        {
            enemy.Initialize(_target, _poolStandartEnemy, _player, this);
            return;
        }

        if (enemy.TryGetComponent(out FastEnemy fastEnemy))
        {
            enemy.Initialize(_target, _poolFastEnemy, _player, this);
            return;
        }

        if (enemy.TryGetComponent(out BigEnemy bigEnemy))
        {
            enemy.Initialize(_target, _poolBigEnemy, _player, this);
            return;
        }
    }

    private bool TyrFindEnemy(IPoolObject enemyType, out Enemy poolEnemy)
    {
        Enemy enemy = enemyType as Enemy;
        poolEnemy = null;

        if (enemy.TryGetComponent(out StandartEnemy standartEnemy))
        {
            if (_poolStandartEnemy.TryPoolObject(out IPoolObject enemyPool))
                poolEnemy = enemyPool as Enemy;
        }

        if (enemy.TryGetComponent(out FastEnemy fastEnemy))
        {
            if (_poolFastEnemy.TryPoolObject(out IPoolObject enemyPool))
                poolEnemy = enemyPool as Enemy;
        }

        if (enemy.TryGetComponent(out BigEnemy bigEnemy))
        {
            if (_poolBigEnemy.TryPoolObject(out IPoolObject enemyPool))
                poolEnemy = enemyPool as Enemy;
        }

        return poolEnemy != null;
    }

    private void SetWave(int index)
    {
        _currentWave = _enemyWaves[index];
        Debug.Log(index);
    }

    private void NextWave()
    {
        _spawned = 0;
        SetWave(++_currentWaveNumber);
        Debug.Log("Вызвал из НекстВейв");
    }

    private void SetWaveComplexity()
    {

        if(_player.CurrentLvl <= EasyWaveIndex)
        {
            _countWaves = 2;
            SetWaves(_countWaves, EasyWaveIndex);
            Debug.Log("установил лешкий");
            return;
        }

        if(_player.CurrentLvl > EasyWaveIndex && _player.CurrentLvl < MidWaveIndex)
        {
            _countWaves = 3;
            SetWaves(_countWaves, MidWaveIndex);
            Debug.Log("установил средний");
            return;
        }

        if (_player.CurrentLvl >= HardWaveIndex)
        {
            _countWaves = 4;
            SetWaves(_countWaves, HardWaveIndex);
            Debug.Log("установил тяжелый");
            return;
        }
    }

    private void SetWaves(int countWave, int complexityWave)
    {
        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < countWave; i++)
        {

            for (int j = 0; j < WaveLenght; j++)
            {
                enemies.Add(_enemiesPrefab.GetEnemy(complexityWave));
            }
            
            _enemyWaves.Add(new EnemyWave(enemies));
        }

        SetWave(_currentWaveNumber);
        OnSpawnerReset?.Invoke();
        Debug.Log("Вызвал из СетВэйвс");
        Debug.Log(_enemyWaves.Count);
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
