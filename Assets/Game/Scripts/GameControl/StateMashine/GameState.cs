using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private List<GameTransition> _transitions;

    protected Player Player;

    public virtual void Enter(Player player)
    {
        if (enabled == false)
        {
            Player = player;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public virtual void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public GameState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }

    private void Awake()
    {
        enabled = false;
    }
}
