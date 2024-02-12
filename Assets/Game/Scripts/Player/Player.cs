using UnityEngine;

[RequireComponent(typeof(MovementPlayer))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;
    [SerializeField] private SizeClipUI _sizeClip;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private TurretPresenter _turretPresenter;
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private TurretAudioSourse _turretAudio;

    private BaseTurret _turret;
    private int _currentMoney;
    private MovementPlayer _movement;

    public BaseTurret CurrentTurret => _turret;
    public int CurrentMoney => _currentMoney;
    public int CurrentLvl => _playerLevel.CurrentPlayerLvl;
    public int CurrenExpereance => _playerLevel.CurrentExperience;
    public int CurrentScore => _playerScore.CurrentScore;
    public float CurrentMoveSpeed => _movement.MoveSpeed;

    public void Initialize()
    {
        _currentMoney = _money.CurrentMoney;
        _money.OnChengetMoney += OnMoneyChenged;
        _movement = GetComponent<MovementPlayer>();
    }

    public void InitializeTurret(BaseTurret turret, int ammouSize, float cooldownReload)
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

    public void RotationTurret(float rotationY)
    {
        _turretPosition.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    public void SetMovmentMode(bool canMove)
    {
        _movement.SetModeMovmen(canMove);
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
