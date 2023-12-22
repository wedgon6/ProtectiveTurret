using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeClipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountBullet;
    [SerializeField] private Image _reloadImage;
    
    private BaseTurret _clip;

    public void SetTurret(BaseTurret turret)
    {
        if(_clip == null)
        {
            _clip = turret;
        }
        else
        {
            _clip.OnClipSizeChanged -= OnAmountChanged;
            _clip = null;
            _clip = turret;
        }

        _clip.OnClipSizeChanged += OnAmountChanged;
        //_amountBullet.text = _clip.CurrentCountBullet.ToString();
        Debug.Log($"clip --- {_clip.CurrentCountBullet}/n text -- {_amountBullet.text}");
        _reloadImage.fillAmount = 1f / 2f;
    }

    private void OnDisable()
    {
        if(_clip != null )
            _clip.OnClipSizeChanged -= OnAmountChanged;
    }

    private void OnAmountChanged()
    {
        _amountBullet.text = _clip.CurrentCountBullet.ToString();
        Debug.Log($"clip изменились пули");
    }
}
