using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField] private List<DialoguePhrase> _dialoguePhrases;
    [SerializeField] private TMP_Text _lable;
    [SerializeField] private Image _icon;



    private bool _isContinueDialog = false;

    public void ContinueDialog()
    {
        _isContinueDialog = true;
    }

    private IEnumerator RunStudyDialog()
    {
        for (int i = 0; i < _dialoguePhrases.Count; i++)
        {
            _isContinueDialog = false;
            _lable.text = _dialoguePhrases[i].Phease;
            _icon.sprite = _dialoguePhrases[i].Icon;

            yield return new WaitUntil(() => _isContinueDialog);
        }
    }
}
