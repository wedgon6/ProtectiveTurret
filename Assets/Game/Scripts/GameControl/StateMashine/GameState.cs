using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] private List<GameTransition> _transitions;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init();
            }
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

    public void Exit()
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
}
