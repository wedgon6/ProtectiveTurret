using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private PlayerMoney _playerMoney;
    private PlayerScore _playerScore;
    private RedLine _target;
    private EnemyState _currentState;
    private Enemy _enemy;

    public PlayerMoney MoneyPlayer => _playerMoney;
    public PlayerScore PlayerScore => _playerScore;
    public EnemyState CurrentState => _currentState;

    public void ResetStete()
    {
        EnterState(_firstState);
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = _enemy.Target;
        _playerScore = _enemy.PlayerScore;
        _playerMoney = _enemy.PlayerMoney;
        EnterState(_firstState);
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

    private void EnterState(EnemyState startState)
    {
        _currentState = startState;

        if (_currentState != null)
        {
            _currentState.Enter(_target, _playerScore, _playerMoney);
        }
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(_target, _playerScore, _playerMoney);
        }
    }
}