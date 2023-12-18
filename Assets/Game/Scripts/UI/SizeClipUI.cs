using TMPro;
using UnityEngine;

public class SizeClipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountBullet;
    
    private BaseTurret _clip;

    public void SetTurret(BaseTurret turret)
    {
        _clip = turret;
        _amountBullet.text = _clip.CurrentCountBullet.ToString();
        Debug.Log($"clip --- {_clip.CurrentCountBullet}/n text -- {_amountBullet.text}");
        _clip.onClipSizeChanged += OnAmountChanged;
    }

    private void OnDisable()
    {
        _clip.onClipSizeChanged -= OnAmountChanged;
    }

    private void OnAmountChanged()
    {
        _amountBullet.text = _clip.CurrentCountBullet.ToString();
    }
}
