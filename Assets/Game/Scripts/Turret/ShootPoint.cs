using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;

    public void PlayParticle()
    {
        _muzzleFlash.Play();
    }
}