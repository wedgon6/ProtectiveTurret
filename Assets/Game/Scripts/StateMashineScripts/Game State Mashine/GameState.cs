namespace ProtectiveTurret.StateMashineScripts
{
    public abstract class GameState : State
    {
        private void Awake()
        {
            enabled = false;
        }
    }
}