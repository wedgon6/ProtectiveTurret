using UnityEngine;

[RequireComponent(typeof(MovementPlayer))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;
    [SerializeField] private SizeClipUI _sizeClip;

    private BaseTurret _turret;
    private int _currentMoney;
    private MovementPlayer _movement;

    public BaseTurret CurrentTurret => _turret;
    public int CurrentMoney => _currentMoney;

    public void Initialize()
    {
        _currentMoney = _money.CurrentMoney;
        _money.OnChengetMoney += OnMoneyChenged;
        _movement = GetComponent<MovementPlayer>();
    }

    public void InitializeTurret(BaseTurret turret)
    {
        if(_turret != null)
        {
            Destroy(_turret.gameObject);
        }

        _turret = Instantiate(turret, _turretPosition.transform.position, _turretPosition.rotation, _turretPosition);
        _sizeClip.SetTurret(_turret);
        _turret.RechargeTurret();
    }

    public void AddMonuy(int money)
    {
        _money.AddMoney(money);
    }

    public void ReduceMoney(int money)
    {
        _money.ReduceMoney(money);
    }

    public void RotationTurret(float rotationY)
    {
        _turretPosition.rotation = Quaternion.Euler(0, rotationY, 0);
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

    private void OnMoneyChenged()
    {
        _currentMoney = _money.CurrentMoney;
    }
}
