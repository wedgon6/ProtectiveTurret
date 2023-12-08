using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTransition : GameTransition
{
    [SerializeField] EnemyPresenter _enemyPresenter;

    private void OnEnable()
    {
        _enemyPresenter.OnAllEnemiesDie += OnNeedTransition;
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
