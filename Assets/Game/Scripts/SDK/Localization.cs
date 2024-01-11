using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public sealed class Localization : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
    }

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        string language = languageCode switch
        {
            EnglishCode => Language.en.ToString(),
            RussianCode => Language.ru.ToString(),
            TurkishCode => Language.tr.ToString(),
            _ => Language.en.ToString()
        };

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}
