using System;

public class AmmoCountAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.AddAmmouSize();
        _currentPrice = (int)Math.Round(_currentPrice * _multiplier, 0);
        _currentLvl++;
        base.Buy(player);
    }
}
