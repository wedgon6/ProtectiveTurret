using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public sealed class Localization : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";

    public void ChangeLanguage(string languageCode)
    {
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
