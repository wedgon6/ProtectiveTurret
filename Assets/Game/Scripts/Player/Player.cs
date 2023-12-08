using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;
    [SerializeField] private PoolBullet _poolBullet;

    private BaseTurret _turret;

    public BaseTurret CurrentTurret => _turret;

    public void Initialize(BaseTurret turret)
    {
        Instantiate(turret,_turretPosition);
        _turret = turret;
    }

    public void AddMonue(int moneu)
    {
        _money.AddMoney(moneu);
    }

    public void RotationTurret(float rotationY)
    {
        _turret.gameObject.transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void ResetTurret()
    {
        if (_turret == null)
            return;

        _turret.RechargeTurret();
    }
}
