using UnityEngine;

[RequireComponent(typeof(MovementPlayer))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;
    [SerializeField] private SizeAmmoView _sizeClip;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private TurretPresenter _turretPresenter;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private TurretAudioSourse _turretAudio;
    [SerializeField] private ParticleSystem _deadEffect;

    private Turret _turret;
    private int _currentMoney;
    private MovementPlayer _movement;

    public Turret CurrentTurret => _turret;
    public int CurrentMoney => _currentMoney;
    public int CurrentLvl => _playerLevel.CurrentPlayerLvl;
    public int CurrenExpereance => _playerLevel.CurrentExperience;
    public int CurrentScore => _playerScore.CurrentScore;
    public float CurrentMoveSpeed => _movement.MoveSpeed;

    public void Initialize()
    {
        _currentMoney = _money.CurrentMoney;
        _money.MoneyChanged += OnMoneyChenged;
        _movement = GetComponent<MovementPlayer>();
    }

    public void SetPlayerData(int playerLvl, int playerMoney, int currentExperiancePlayer, float playerMoveSpeed,int currentScore)
    {
        _playerLevel.SetData(playerLvl, currentExperiancePlayer);
        _money.SetMoney(playerMoney);
        _movement.SetMoveSpeed(playerMoveSpeed);
        _playerScore.SetScoreData(currentScore);
    }

    public void InitializeTurret(Turret turret, int ammouSize, float cooldownReload)
    {
        if(_turret != null)
            Destroy(_turret.gameObject);

        _turret = Instantiate(turret, _turretPosition.transform.position, _turretPosition.rotation, _turretPosition);
        _turret.SetTurretParameters(ammouSize, cooldownReload);
        _sizeClip.SetTurret(_turret);
        _turretAudio.SetTurret(_turret);
        _turret.RechargeTurret();
    }

    public void AddMoney(int money)
    {
        _money.AddMoney(money);
    }

    public void AddScore(int score)
    {
        _playerScore.AddScore(score);
    }

    public void ReduceMoney(int money)
    {
        _money.ReduceMoney(money);
    }

    public void SetMovmentMode(bool canMove)
    {
        _movement.SetModeMovmen(canMove);
    }

    public void LoseGame(bool activStatus)
    {
        _turret.gameObject.SetActive(activStatus);
        _turret.transform.rotation = _turretPosition.rotation;
        _movement.SetModeMovmen(activStatus);

        if (activStatus == false)
            _deadEffect.Play();
    }

    public void ResetTurret()
    {
        if (_turret == null)
            return;

        _turret.RechargeTurret();
    }

    public void BoostMoveSpeed()
    {
        _movement.AddMoveSpeed();
    }

    public void AddAmmouSize()
    {
        _turretPresenter.AddAmmouTurret();
    }

    public void DiscountCooldownReload()
    {
        _turretPresenter.ReduceCooldownReload();
    }

    private void OnMoneyChenged()
    {
        _currentMoney = _money.CurrentMoney;
    }
}
