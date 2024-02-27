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
    [SerializeField] private MenuTransition _menuTransition;

    private bool _isContinueDialog = false;
    private Coroutine _corontine;

    public void ContinueDialog()
    {
        _isContinueDialog = true;
    }

    public void StartDialogue()
    {
        if (_corontine != null)
            StopCoroutine(_corontine);

        _corontine = StartCoroutine(RunStudyDialog());
    }

    private IEnumerator RunStudyDialog()
    {
        for (int i = 0; i < _dialoguePhrases.Count; i++)
        {
            _isContinueDialog = false;
            _lable.text = _dialoguePhrases[i].Phease.text;
            _icon.sprite = _dialoguePhrases[i].Icon;

            yield return new WaitUntil(() => _isContinueDialog);
        }

        EndDialogue();
    }

    private void EndDialogue()
    {
        _menuTransition.ReturnToMeny();
    }
}
