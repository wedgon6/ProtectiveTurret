using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void OpenSettingsPannel()
    {
        _settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettingsPanel()
    {
        _settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
