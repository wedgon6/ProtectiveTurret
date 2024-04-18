namespace ProtectiveTurret.StateMashineScripts
{
    public class MenuTransition : Transition
    {
        public void ReturnToMeny()
        {
            NeedTransit = true;
        }
    }
}