using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTransition : GameTransition
{
    [SerializeField] private RedLine _redLine;

    private void OnEnable()
    {
        _redLine.onLooseGame += OnNeedTransition;
    }

    private void OnDisable()
    {
        _redLine.onLooseGame -= OnNeedTransition;
    }

    private void OnNeedTransition()
    {

    }
}
