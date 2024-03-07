using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _leaderbardPanel;
    [SerializeField] private GameObject _setingsPanel;

    public void ClosePanel()
    {
        _setingsPanel.SetActive(true);
        _leaderbardPanel.SetActive(false);
    }
}
