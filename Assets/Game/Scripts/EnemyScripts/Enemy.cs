using ProtectiveTurret.Map;
using ProtectiveTurret.PlayerScripts;
using ProtectiveTurret.PoolSystem;
using ProtectiveTurret.StateMashineScripts;
using UnityEngine;

namespace ProtectiveTurret.EnemyScripts
{
    public class Enemy : PoolObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private int _revard;
        [SerializeField] private int _countScore;
        [SerializeField] private GameObject _backlight;
        [SerializeField] private GameObject _deadParticle;
        [SerializeField] private TypeEnemy _typeEnemy;

        private RedLine _target;
        private float _health;
        private bool _isDead;
        private EnemySpawner _spawner;
        private PlayerScore _playerScore;
        private PlayerMoney _playerMoney;

        public RedLine Target => _target;
        public PlayerScore PlayerScore => _playerScore;
        public PlayerMoney PlayerMoney => _playerMoney;
        public EnemySpawner Spawner => _spawner;
        public float Health => _health;
        public int Revard => _revard;
        public int CountScore => _countScore;
        public bool IsDead => _isDead;
        public string TypeEnemy => _typeEnemy.ToString();

        public void Initialize(RedLine target, EnemySpawner spawner, PlayerScore playerScore, PlayerMoney playerMoney)
        {
            _target = target;
            _health = _maxHealth;
            _spawner = spawner;
            _playerScore = playerScore;
            _playerMoney = playerMoney;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                return;

            if (_health < 0)
                return;

            _health -= damage;

            if (_health < 0)
            {
                _health = 0;
                _isDead = true;
            }
        }

        public void ResetState() => _stateMachine.ResetStete();

        public void GetCrosshairs() => _backlight.SetActive(true);

        public void Dead()
        {
            Instantiate(_deadParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            ReturnToPool();
        }

        protected override void ReturnToPool()
        {
            base.ReturnToPool();
            _backlight.SetActive(false);
            _health = _maxHealth;
            _isDead = false;
        }
    }
}