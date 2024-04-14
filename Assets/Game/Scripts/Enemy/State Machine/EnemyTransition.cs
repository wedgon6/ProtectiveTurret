using UnityEngine;
public abstract class EnemyTransition : MonoBehaviour
{
    protected RedLine Target { get; private set; }

    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(RedLine target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}