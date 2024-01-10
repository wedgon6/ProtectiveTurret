using System;

public class MoveSpeedAbillity : PlayerAbillity
{
    public override void Buy(Player player)
    {
        player.BoostMoveSpeed();
        _currentPrice = (int)Math.Round(_currentPrice * _multiplier, 0);
        _currentLvl++;
        OnPriceChenget?.Invoke();
        OnLvlChenget?.Invoke();
    }
}
