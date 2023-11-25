public class TurretLvl_1 : AbstractTurret
{
    protected int _clipSizeLvl1 = 50;
    protected int _cooldownLvl1 = 2;

    private void Awake()
    {
        _clipSize = _clipSizeLvl1;
        _cooldown = _cooldownLvl1;
        _currentCoutBullet = _clipSize;
    }

    private void FixedUpdate()
    {
        //if(_currentTarget != null && _currentTarget.isActiveAndEnabled != false)
        //{
        //    LookAtEnemy();
        //}
        //else
        //{
        //    FindTarget();
        //}
        ShootingControl();
    }

    protected override void FindTarget()
    {
        base.FindTarget();
        //CorountineStart(Shooting());
    }
}
