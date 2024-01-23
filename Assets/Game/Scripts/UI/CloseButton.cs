using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}
