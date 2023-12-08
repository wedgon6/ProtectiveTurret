using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : GameState
{
    [SerializeField] private WinGamePanel _winGamePanel;

    public override void Enter(Player player)
    {
        base.Enter(player);
        _winGamePanel.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _winGamePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
