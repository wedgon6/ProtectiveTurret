using Lean.Localization;
using UnityEngine;

public sealed class Localization : MonoBehaviour
{
    private const string English = "English";
    private const string Russian = "Russian";
    private const string Turkish = "Turkish";

    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    [SerializeField] private LeanLocalization _leanLocalization;

    public void ChangeLanguage(string languageCode)
    {
        //string language = languageCode switch
        //{
        //    EnglishCode => Language.en.ToString(),
        //    RussianCode => Language.ru.ToString(),
        //    TurkishCode => Language.tr.ToString(),
        //    _ => Language.en.ToString()
        //};

        string language = English;

        switch (languageCode)
        {
            case EnglishCode:
                language = English;
                break;
            case RussianCode:
                language = Russian;
                break;
            case TurkishCode:
                language = Turkish;
                break;
        }

        _leanLocalization.SetCurrentLanguage(language);
        Debug.Log("Chenge language" + language);
    }
}
