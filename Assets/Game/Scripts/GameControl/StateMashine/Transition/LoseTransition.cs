using UnityEngine;

public class LoseTransition : GameTransition
{
    [SerializeField] private RedLine _redLine;

    private void OnEnable()
    {
        NeedTransit = false;
        _redLine.OnLooseGame += OnNeedTransition;
    }

    private void OnDisable()
    {
        _redLine.OnLooseGame -= OnNeedTransition;
    }

    private void OnNeedTransition()
    {
        NeedTransit = true;
    }
}
