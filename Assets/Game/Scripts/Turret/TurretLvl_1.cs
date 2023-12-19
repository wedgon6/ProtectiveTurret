using UnityEngine;

public class TurretLvl_1 : BaseTurret
{
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ShootingControl();
    }

    protected override void FindTarget()
    {
        base.FindTarget();
    }
}
