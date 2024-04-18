using UnityEngine;
using UnityEngine.UI;

namespace ProtectiveTurret.GameControl
{
    public class WaveProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _bar;
        [SerializeField] private EnemyCounter _enemyCounter;

        private void OnEnable()
        {
            _enemyCounter.EnemiesDied += OnEnemiesDeadCountChenget;
        }

        private void OnDisable()
        {
            _enemyCounter.EnemiesDied -= OnEnemiesDeadCountChenget;
        }

        private void OnEnemiesDeadCountChenget(int deadEnemise, int totalCountEnemise)
        {
            float amount = deadEnemise / (float)totalCountEnemise;
            _bar.value = amount;
        }
    }
}