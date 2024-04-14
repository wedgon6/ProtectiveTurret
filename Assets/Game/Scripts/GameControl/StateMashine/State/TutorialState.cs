using UnityEngine;

public class TutorialState : GameState
{
    [SerializeField] private DialoguePresenter _dialogue;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _dialogue.gameObject.SetActive(true);
        _dialogue.StartDialogue();
    }

    public override void Exit()
    {
        _dialogue.gameObject.SetActive(false);
        base.Exit();
    }
}