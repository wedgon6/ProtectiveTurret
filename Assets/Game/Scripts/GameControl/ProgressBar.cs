using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private EnemyPresenter _enemyPresenter;

    private void OnEnable()
    {
        _enemyPresenter.OnEnemyDie += OnEnemiesDeadCountChenget;
    }

    private void OnDisable()
    {
        _enemyPresenter.OnEnemyDie -= OnEnemiesDeadCountChenget;
    }

    private void OnEnemiesDeadCountChenget(int deadEnemise, int totalCountEnemise)
    {
        float amount = (float)deadEnemise / (float)totalCountEnemise;
        _bar.value = amount;
    }
}
