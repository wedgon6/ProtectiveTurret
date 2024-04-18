using ProtectiveTurret.EnemyScripts;
using System;
using UnityEngine;

namespace ProtectiveTurret.GameControl
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;

        private int _countEnemy;
        private int _deadEnemiesCount;

        public event Action AllEnemiesDied;
        public event Action<int, int> EnemiesDied;

        private void OnEnable()
        {
            _spawner.OnEnemyDead += OnEnemyDead;
            _spawner.OnSpawnerReset += OnResetWave;
        }

        private void OnDisable()
        {
            _spawner.OnEnemyDead -= OnEnemyDead;
            _spawner.OnSpawnerReset -= OnResetWave;
        }

        private void OnEnemyDead()
        {
            _deadEnemiesCount++;
            EnemiesDied?.Invoke(_deadEnemiesCount, _countEnemy);

            if (_deadEnemiesCount == _countEnemy)
                AllEnemiesDied?.Invoke();
        }

        private void OnResetWave()
        {
            _countEnemy = _spawner.GetEnemyCount();
            _deadEnemiesCount = 0;
            EnemiesDied.Invoke(_deadEnemiesCount, _countEnemy);
        }
    }
}