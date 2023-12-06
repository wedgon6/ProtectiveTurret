using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Transform _turretPosition;

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
}
