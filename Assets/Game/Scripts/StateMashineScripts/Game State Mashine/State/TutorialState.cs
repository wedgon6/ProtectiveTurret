using ProtectiveTurret.GameControl;
using UnityEngine;

namespace ProtectiveTurret.StateMashineScripts
{
    public class TutorialState : GameState
    {
        [SerializeField] private DialoguePresenter _dialogue;

        public override void Enter()
        {
            base.Enter();
            _dialogue.gameObject.SetActive(true);
            _dialogue.StartDialogue();
        }

        public override void Exit()
        {
            _dialogue.gameObject.SetActive(false);
            base.Exit();
        }
    }
}