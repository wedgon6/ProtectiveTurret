using System;

[Serializable]
public class GameInfo
{
    private Player _player;
    private int _playerLvl;
    private int _playerMoney;
    private int _currentExperiancePlayer;
    private float _playerMoveSpeed;

    public GameInfo(Player player)
    {
        _player = player;
    }

    public int PlayerLvl => _playerLvl;
    public int PlayerMoney => _playerMoney; 
    public int CurrentExperiancePlayer => _currentExperiancePlayer;
    public float PlayerMoveSpeed => _playerMoveSpeed;

    public void GetPlayerData()
    {
        _playerLvl = _player.CurrentLvl;
        _playerMoney = _player.CurrentMoney;
        _currentExperiancePlayer = _player.CurrenExpereance;
        _playerMoveSpeed = _player.CurrentMoveSpeed;
    }
}
