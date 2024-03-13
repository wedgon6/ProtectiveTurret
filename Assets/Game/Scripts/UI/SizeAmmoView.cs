using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeAmmoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountBullet;
    [SerializeField] private Image _reloadImage;
    
    private BaseTurret _clip;
    private Coroutine _rechargeCoroutine;

    public void SetTurret(BaseTurret turret)
    {
        if(_clip == null)
        {
            _clip = turret;
        }
        else
        {
            _clip.OnClipSizeChanged -= OnAmountChanged;
            _clip.OnRechargeAmmou -= OnRecharge;
            _clip = null;
            _clip = turret;
        }

        _clip.OnClipSizeChanged += OnAmountChanged;
        _clip.OnRechargeAmmou += OnRecharge;
    }

    private void OnEnable()
    {
        _clip.OnClipSizeChanged += OnAmountChanged;
        _clip.OnRechargeAmmou += OnRecharge;
        _reloadImage.fillAmount = 0f;
    }

    private void OnDisable()
    {
        if(_clip != null)
        {
            _clip.OnClipSizeChanged -= OnAmountChanged;
            _clip.OnRechargeAmmou -= OnRecharge;
        }

        _reloadImage.fillAmount = 0f;
    }

    private void OnAmountChanged()
    {
        _amountBullet.text = _clip.CurrentCountBullet.ToString();
    }

    private void OnRecharge()
    {
        if (_rechargeCoroutine != null)
            StopCoroutine(_rechargeCoroutine);

        _rechargeCoroutine = StartCoroutine(RechargeAnimation());
    }

    private IEnumerator RechargeAnimation()
    {
        _reloadImage.DOFillAmount(1, _clip.CoolDonw).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_clip.CoolDonw);
        _reloadImage.fillAmount = 0f;
    }
}
