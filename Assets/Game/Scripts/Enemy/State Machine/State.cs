using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    protected ReadLine Target { get; set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Enter(ReadLine target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNextState()
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
