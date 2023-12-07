using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;

    private BaseTurret _turret;

    public void Initialize(BaseTurret turret)
    {
        Instantiate(turret,_turretPosition);
    }

    public void AddMonue(int moneu)
    {
        _money.AddMoney(moneu);
    }

    public void RotationTurret(float rotationY)
    {
        _turretPosition.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void ResetTurret()
    {
        if (_turret == null)
            return;

        _turret.RechargeTurret();
    }
}
