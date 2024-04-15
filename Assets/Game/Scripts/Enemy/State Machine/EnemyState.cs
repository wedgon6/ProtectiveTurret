using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private List<EnemyTransition> _transitions;

    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    protected RedLine Target { get; set; }
    protected PlayerScore PlayerScore { get; set; }
    protected PlayerMoney PlayerMoney { get; set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Enter(RedLine target, PlayerScore playerScore, PlayerMoney playerMoney)
    {
        if (enabled == false)
        {
            Target = target;
            PlayerScore = playerScore;
            PlayerMoney = playerMoney;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public EnemyState GetNextState()
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
