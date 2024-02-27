using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Image _icon;

    public void Render(DialoguePhrase phrase)
    {
        _lable.text = phrase.Phease.text;
        _icon.sprite = phrase.Icon;
    }

    public void Clear()
    {
        _lable = null;
        _icon = null;
    }
}
