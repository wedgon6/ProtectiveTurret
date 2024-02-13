using System;
using System.Collections.Generic;

[Serializable]
public class GameInfo
{
    private Player _player;
    private int _playerLvl;
    private int _playerMoney;
    private int _currentExperiancePlayer;
    private float _playerMoveSpeed;
    private int _playerScore;

    private List<PlayerAbillity> _playerAbillities;

    public GameInfo(Player player, List<PlayerAbillity> playerAbillities)
    {
        _player = player;
        _playerAbillities = playerAbillities;
    }

    public int PlayerLvl => _playerLvl;
    public int PlayerMoney => _playerMoney; 
    public int CurrentExperiancePlayer => _currentExperiancePlayer;
    public float PlayerMoveSpeed => _playerMoveSpeed;
    public int PlayerScore => _playerScore;
    public List<PlayerAbillity> PlayerAbillities => _playerAbillities;

    public void GetPlayerData()
    {
        _playerLvl = _player.CurrentLvl;
        _playerMoney = _player.CurrentMoney;
        _currentExperiancePlayer = _player.CurrenExpereance;
        _playerMoveSpeed = _player.CurrentMoveSpeed;
        _playerScore = _player.CurrentScore;
    }
}
