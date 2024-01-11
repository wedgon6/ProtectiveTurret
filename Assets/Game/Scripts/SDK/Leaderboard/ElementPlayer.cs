using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string name, int rank, int score)
    {
        _playerName.text = name;
        _playerRank.text = rank.ToString();
        _playerScore.text = score.ToString();
    }

}
