using ProtectiveTurret.PlayerScripts;

namespace ProtectiveTurret.ShoopAbillity
{
    public class AmmoCountAbillity : PlayerAbillity
    {
        public override void Buy(Player player)
        {
            player.AddAmmouSize();
            base.Buy(player);
        }
    }
}