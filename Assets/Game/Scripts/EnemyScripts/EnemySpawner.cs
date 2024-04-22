using ProtectiveTurret.Map;
using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.PoolSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectiveTurret.EnemyScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        private const int EasyWaveIndex = 3;
        private const int MidWaveIndex = 7;
        private const int HardWaveIndex = 11;
        private const int WaveLenght = 5;
        private const float EasyWaveDelay = 1.1f;
        private const float MidWaveDelay = 0.9f;
        private const float HardWaveDelay = 0.7f;

        [SerializeField] private RedLine _target;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerScore _playerScore;
        [SerializeField] private PlayerMoney _playerMoney;
        [SerializeField] private EnemiesList _enemiesPrefab;

        [SerializeField] private Pool _poolStandartEnemy;
        [SerializeField] private Pool _poolFastEnemy;
        [SerializeField] private Pool _poolBigEnemy;

        private EnemyWave _currentWave = null;
        private int _currentWaveNumber = 0;
        private int _countWaves;
        private float _timeAfterLastSpawn;
        private float _delay;
        private int _spawned;
        private Coroutine _corontine;
        private List<PoolObject> _createdEnemies = new List<PoolObject>();
        private List<EnemyWave> _enemyWaves = new List<EnemyWave>();

        public event Action OnEnemyDead;
        public event Action OnSpawnerReset;

        public void EnemyDead()
        {
            OnEnemyDead?.Invoke();
        }

        public void PutEnemyToPool()
        {
            _currentWave = null;

            if (_createdEnemies.Count > 0)
            {
                foreach (var enemy in _createdEnemies)
                {
                    enemy.ReturObjectPool();
                }
            }
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
                return;
            }

            _timeAfterLastSpawn += Time.deltaTime;

            if (_timeAfterLastSpawn >= _delay)
            {
                InitializeEnemy();
                _spawned++;
                _timeAfterLastSpawn = 0;
            }

            if (_currentWave.Template == null)
                return;

            if (_currentWave.Template.Count <= _spawned)
            {
                _currentWave = null;

                if (_enemyWaves.Count > _currentWaveNumber + 1)
                    CorountineStart(StartNextWave());
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
                enemy = Instantiate(enemy, _spawnPoint[currentSpawnPont].position, _spawnPoint[currentSpawnPont].rotation, _spawnPoint[currentSpawnPont]).GetComponent<Enemy>();
                InitializeEnemy(enemy);
                _createdEnemies.Add(enemy);
            }
        }

        private void InitializeEnemy(Enemy enemy)
        {
            if (enemy.TryGetComponent(out Enemy standartEnemy))
            {
                enemy.Initialize(_target, this, _playerScore, _playerMoney);
                _poolStandartEnemy.InstantiatePoolObject(enemy);
                return;
            }

            if (enemy.TryGetComponent(out Enemy fastEnemy))
            {
                enemy.Initialize(_target, this, _playerScore, _playerMoney);
                _poolFastEnemy.InstantiatePoolObject(enemy);
                return;
            }

            if (enemy.TryGetComponent(out Enemy bigEnemy))
            {
                enemy.Initialize(_target, this, _playerScore, _playerMoney);
                _poolBigEnemy.InstantiatePoolObject(enemy);
                return;
            }
        }

        private bool TyrFindEnemy(PoolObject enemyType, out Enemy poolEnemy)
        {
            Enemy enemy = enemyType as Enemy;
            poolEnemy = null;

            if (enemy.TypeEnemy == TypeEnemy.Standart.ToString())
            {
                if (_poolStandartEnemy.TryPoolObject(out PoolObject enemyPool))
                    poolEnemy = enemyPool as Enemy;
            }

            if (enemy.TypeEnemy == TypeEnemy.Fast.ToString())
            {
                if (_poolFastEnemy.TryPoolObject(out PoolObject enemyPool))
                    poolEnemy = enemyPool as Enemy;
            }

            if (enemy.TypeEnemy == TypeEnemy.Big.ToString())
            {
                if (_poolBigEnemy.TryPoolObject(out PoolObject enemyPool))
                    poolEnemy = enemyPool as Enemy;
            }

            return poolEnemy != null;
        }

        private void SetWave(int index)
        {
            _currentWave = _enemyWaves[index];
        }

        private void NextWave()
        {
            _spawned = 0;
            SetWave(++_currentWaveNumber);
        }

        private void SetWaveComplexity()
        {
            if (_player.CurrentLvl <= EasyWaveIndex)
            {
                _countWaves = 2;
                SetWaves(_countWaves, EasyWaveIndex);
                _delay = EasyWaveDelay;
                return;
            }

            if (_player.CurrentLvl > EasyWaveIndex && _player.CurrentLvl <= MidWaveIndex)
            {
                _countWaves = 3;
                SetWaves(_countWaves, MidWaveIndex);
                _delay = MidWaveDelay;
                return;
            }

            if (_player.CurrentLvl > MidWaveIndex)
            {
                _countWaves = 4;
                SetWaves(_countWaves, HardWaveIndex);
                _delay = HardWaveDelay;
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
}