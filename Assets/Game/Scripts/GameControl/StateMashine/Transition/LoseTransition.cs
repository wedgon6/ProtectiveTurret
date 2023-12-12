using UnityEngine;

public class LoseTransition : GameTransition
{
    [SerializeField] private RedLine _redLine;

    private void OnEnable()
    {
        NeedTransit = false;
        _redLine.onLooseGame += OnNeedTransition;
    }

    private void OnDisable()
    {
        _redLine.onLooseGame -= OnNeedTransition;
    }

    private void OnNeedTransition()
    {
        NeedTransit = true;
    }
}
