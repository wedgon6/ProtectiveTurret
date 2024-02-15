using System;
using System.Collections.Generic;

[Serializable]
public class GameInfo
{
    private Player _player;
    public int PlayerLvl;
    public int PlayerMoney;
    public int CurrentExperiancePlayer;
    public float PlayerMoveSpeed;
    public int PlayerScore;

    private List<PlayerAbillity> _playerAbillities;

    public GameInfo(Player player, List<PlayerAbillity> playerAbillities)
    {
        _player = player;
        _playerAbillities = playerAbillities;
    }
    public List<PlayerAbillity> PlayerAbillities => _playerAbillities;

    public void GetPlayerData()
    {
        PlayerLvl = _player.CurrentLvl;
        PlayerMoney = _player.CurrentMoney;
        CurrentExperiancePlayer = _player.CurrenExpereance;
        PlayerMoveSpeed = _player.CurrentMoveSpeed;
        PlayerScore = _player.CurrentScore;
    }
}
