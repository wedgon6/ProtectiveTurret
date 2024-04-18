namespace ProtectiveTurret.StateMashineScripts
{
    public class BattleTransition : Transition
    {
        public void StartBattle()
        {
            NeedTransit = true;
        }
    }
}