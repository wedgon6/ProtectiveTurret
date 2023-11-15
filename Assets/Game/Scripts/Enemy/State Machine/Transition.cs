using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected ReadLine Target { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(ReadLine target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
