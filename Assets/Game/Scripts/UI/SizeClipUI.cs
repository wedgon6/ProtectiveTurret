using TMPro;
using UnityEngine;

public class SizeClipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountBullet;
    
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
