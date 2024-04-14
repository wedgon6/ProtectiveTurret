using System;
using UnityEngine;

public class RedLine : MonoBehaviour
{
    public event Action GameLoosed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            GameLoosed?.Invoke();
    }
}