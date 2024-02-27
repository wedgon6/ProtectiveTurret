using TMPro;
using UnityEngine;

public class DialoguePhrase : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private TMP_Text _phease;

    public TMP_Text Phease => _phease;
    public Sprite Icon => _icon;
}
