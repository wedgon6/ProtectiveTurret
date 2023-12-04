using UnityEngine;

public class TurretLvl_1 : BaseTurret
{
    protected int _clipSizeLvl1 = 50;
    protected float _cooldownLvl1 = 1.5f;

    private void Awake()
    {
        _clipSize = _clipSizeLvl1;
        _cooldown = _cooldownLvl1;
        _currentCoutBullet = _clipSize;
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
