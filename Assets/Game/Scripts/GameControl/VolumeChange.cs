using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    [SerializeField] private Image _controlButton;
    [SerializeField] private Sprite _onAudioIcon;
    [SerializeField] private Sprite _offAudioIcon;
    [SerializeField] private AudioSource _audioSource;

    private bool _isAudioPlay = true;

    public bool IsAudioPlay => _isAudioPlay;

   public void StartPlayMusic()
    {
        _audioSource.Play();
        _audioSource.volume = 1f;
        AudioListener.volume = 1f;
        _audioSource.playOnAwake = true;
        Debug.Log("PlayMusic");
        Debug.Log($"{_audioSource.volume} -  _audioSource.volume//{AudioListener.volume} - AudioListener.volume");
    }

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
