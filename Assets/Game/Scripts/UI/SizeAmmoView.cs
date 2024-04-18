using System.Collections;
using DG.Tweening;
using ProtectiveTurret.TurretScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProtectiveTurret.UI
{
    public class SizeAmmoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountBullet;
        [SerializeField] private Image _reloadImage;

        private Turret _clip;
        private Coroutine _rechargeCoroutine;

        public void SetTurret(Turret turret)
        {
            if (_clip == null)
            {
                _clip = turret;
            }
            else
            {
                _clip.ClipSizeChanged -= OnAmountChanged;
                _clip.AmmouRecharging -= OnRecharge;
                _clip = null;
                _clip = turret;
            }

            _clip.ClipSizeChanged += OnAmountChanged;
            _clip.AmmouRecharging += OnRecharge;
        }

        private void OnEnable()
        {
            _clip.ClipSizeChanged += OnAmountChanged;
            _clip.AmmouRecharging += OnRecharge;
            _reloadImage.fillAmount = 0f;
        }

        private void OnDisable()
        {
            if (_clip != null)
            {
                _clip.ClipSizeChanged -= OnAmountChanged;
                _clip.AmmouRecharging -= OnRecharge;
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
}