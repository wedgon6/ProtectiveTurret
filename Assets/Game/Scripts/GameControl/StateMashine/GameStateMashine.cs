using UnityEngine;
using Agava.YandexGames;

public class GameStateMashine : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameState _firstState;

    private GameState _currentState;

    public GameState CurrentState => _currentState;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
    }

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        var nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Reset(GameState startState)
    {
        _currentState = startState;

        if (_currentState != null)
        {
            _currentState.Enter(_player);
        }
    }

    private void Transit(GameState nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(_player);
        }
    }
}
