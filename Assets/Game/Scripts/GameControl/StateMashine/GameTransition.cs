using UnityEngine;

public abstract class GameTransition : MonoBehaviour
{
    [SerializeField] private GameState _targetState;

    public GameState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init()
    {
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
