using UnityEngine;

public class WinTransition : GameTransition
{
    [SerializeField] private EnemyPresenter _enemyPresenter;

    private void OnEnable()
    {
        _enemyPresenter.OnAllEnemiesDie += OnNeedTransition;
        NeedTransit = false;
    }

    private void OnDisable()
    {
        _enemyPresenter.OnAllEnemiesDie -= OnNeedTransition;
    }

    private void OnNeedTransition()
    {
        NeedTransit = true;
    }
}
