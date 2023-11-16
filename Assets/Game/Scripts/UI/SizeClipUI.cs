using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SizeClipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountBullet;
    [SerializeField] private AbstractTurret _clip;

    private void Start()
    {
        _amountBullet.text = _clip.CurrentSizeClip.ToString(); 
    }

    private void OnEnable()
    {
        _clip.onClipSizeChanged += OnAmountChanged;
    }

    private void OnDisable()
    {
        _clip.onClipSizeChanged -= OnAmountChanged;
    }

    private void OnAmountChanged()
    {
        _amountBullet.text = _clip.CurrentSizeClip.ToString();
    }
}
