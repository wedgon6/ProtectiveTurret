using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLvlProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private PlayerLevel _playerLvlProgress;
    [SerializeField] private TMP_Text _playerLvlValue;

    private void OnEnable()
    {
        _playerLvlValue.text = _playerLvlProgress.CurrentPlayerLvl.ToString();
        OnPlayerLvlProgressChenget(_playerLvlProgress.CurrentExperience,_playerLvlProgress.ExperienceToNextLvl);
        _playerLvlProgress.PlayerExpirianceChanged += OnPlayerLvlProgressChenget;
        _playerLvlProgress.LvLPlayerSet += OnPlayerLvlUp;
        _playerLvlProgress.PlayerLvlChanged += OnPlayerLvlUp;
    }

    private void OnDisable()
    {
        _playerLvlProgress.PlayerExpirianceChanged -= OnPlayerLvlProgressChenget;
        _playerLvlProgress.LvLPlayerSet -= OnPlayerLvlUp;
        _playerLvlProgress.PlayerLvlChanged -= OnPlayerLvlUp;
    }

    private void OnPlayerLvlProgressChenget(int currentValue, int totalValue)
    {
        float amount = (float)currentValue / (float)totalValue;
        _bar.value = amount;
    }

    private void OnPlayerLvlUp() => _playerLvlValue.text = _playerLvlProgress.CurrentPlayerLvl.ToString();
}
