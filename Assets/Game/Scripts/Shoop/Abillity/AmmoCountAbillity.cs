using System;

public class AmmoCountAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.AddAmmouSize();
        base.Buy(player);
    }
}
