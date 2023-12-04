using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLine : MonoBehaviour
{
    public Action onLooseGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            onLooseGame?.Invoke();
        }

    }
}
