using UnityEngine;

namespace ProtectiveTurret.UI
{
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderbardPanel;

        public void ClosePanel()
        {
            _leaderbardPanel.SetActive(false);
        }
    }
}