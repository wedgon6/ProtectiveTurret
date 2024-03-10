using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    [SerializeField] private Image _controlButton;
    [SerializeField] private Sprite _onAudioIcon;
    [SerializeField] private Sprite _offAudioIcon;

    private bool _isAudioPlay = true;

    public bool IsAudioPlay => _isAudioPlay;

    public void ChengeAudioPlay()
    {
        _isAudioPlay = !_isAudioPlay;

        if(_isAudioPlay == true)
        {
            AudioListener.volume = 1f;
            _controlButton.sprite = _onAudioIcon;
        }

        if(_isAudioPlay == false)
        {
            AudioListener.volume = 0f;
            _controlButton.sprite = _offAudioIcon;
        }
    }
}
