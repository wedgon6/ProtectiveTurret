using UnityEngine;

public class WinTransition : GameTransition
{
    [SerializeField] private EnemyCounter _enemyCounter;

    private void OnEnable()
    {
        _enemyCounter.AllEnemiesDied += OnNeedTransition;
        NeedTransit = false;
    }

    private void OnDisable()
    {
        _enemyCounter.AllEnemiesDied -= OnNeedTransition;
    }

    private void OnNeedTransition()
    {
        NeedTransit = true;
    }
}
