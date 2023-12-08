using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPanels : MonoBehaviour
{
    [SerializeField] private LoseGamePanel _looseGame;
    [SerializeField] private WinGamePanel _winGame;

    public void ShowResult(bool isActive)
    {
        if (isActive)
            _winGame.gameObject.SetActive(true);
        else
            _looseGame.gameObject.SetActive(true);
    }
}
