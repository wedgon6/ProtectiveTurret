using UnityEngine;

public class DialoguePhrase : MonoBehaviour
{
    [TextArea(3,10)]
    [SerializeField] private string _phease;
    [SerializeField] private Sprite _icon;

    public string Phease => _phease;
    public Sprite Icon => _icon;
}
