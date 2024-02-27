using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfo
{
    private Player _player;
    private Shoop _shoop;

    public int PlayerLvl;
    public int PlayerMoney;
    public int CurrentExperiancePlayer;
    public float PlayerMoveSpeed;
    public int PlayerScore;
    public List<int> AbilitiesLvl = new List<int>();
    public List<int> AbilitiesPrise = new List<int>();

    public GameInfo(Player player, Shoop shoop)
    {
        _player = player;
        _shoop = shoop;
    }

    public void GetPlayerData()
    {
        PlayerLvl = _player.CurrentLvl;
        PlayerMoney = _player.CurrentMoney;
        CurrentExperiancePlayer = _player.CurrenExpereance;
        PlayerMoveSpeed = _player.CurrentMoveSpeed;
        PlayerScore = _player.CurrentScore;

        for (int i = 0; i < _shoop.Abillities.Count; i++)
        {
            AbilitiesLvl.Add(_shoop.Abillities[i].CurrentLvl);
            Debug.Log(i);
        }
        
        for (int i = 0; i < _shoop.Abillities.Count; i++)
        {
            AbilitiesLvl.Add(_shoop.Abillities[i].Price);
        }
    }
}
