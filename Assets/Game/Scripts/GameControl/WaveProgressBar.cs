using UnityEngine;
using UnityEngine.UI;

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
        float amount = (float)deadEnemise / (float)totalCountEnemise;
        _bar.value = amount;
    }
}
