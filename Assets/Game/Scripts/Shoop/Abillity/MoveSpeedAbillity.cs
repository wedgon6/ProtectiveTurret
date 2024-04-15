using System;

public class MoveSpeedAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.BoostMoveSpeed();
        base.Buy(player);
    }
}
