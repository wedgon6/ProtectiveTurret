using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class SaveRoot : MonoBehaviour
{
    [SerializeField] private InputField _cloudSaveDataInputField;

    public void OnSetCloudSaveData()
    {
        PlayerAccount.SetCloudSaveData(_cloudSaveDataInputField.text);
    }

    public void OnGetCloudSaveData()
    {
        PlayerAccount.GetCloudSaveData((data) => _cloudSaveDataInputField.text = data);
    }
}
