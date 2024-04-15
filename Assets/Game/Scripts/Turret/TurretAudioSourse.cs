using System.Collections.Generic;
using UnityEngine;

public class TurretAudioSourse : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _shootSounds;

    private int _indexSound = 0;
    private Turret _baseTurret;

    public void SetTurret(Turret turret)
    {
        if (_baseTurret == null)
        {
            _baseTurret = turret;
        }
        else
        {
            _baseTurret.Shoted -= OnPlayShotSound;
            _baseTurret = null;
            _baseTurret = turret;
        }

        _baseTurret.Shoted += OnPlayShotSound;
    }

    private void OnDisable()
    {
        if (_baseTurret != null)
        {
            _baseTurret.Shoted -= OnPlayShotSound;
        }
    }

    private void OnPlayShotSound()
    {
        _shootSounds[_indexSound].Play();
        _indexSound++;

        if (_indexSound >= _shootSounds.Count)
            _indexSound = 0;
    }
}