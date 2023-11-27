using UnityEngine;
using UnityEngine.UI;

public class LooseGamePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
        Time.timeScale = 0;
    }
}
