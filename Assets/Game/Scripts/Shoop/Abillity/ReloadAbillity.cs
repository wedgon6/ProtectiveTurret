using System;

public class ReloadAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.DiscountCooldownReload();
        base.Buy(player);
    }
}
