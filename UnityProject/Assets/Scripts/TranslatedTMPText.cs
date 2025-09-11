using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace ToyGame
{
    public class TranslatedTMPText : MonoBehaviour
    {
        [SerializeField] private List<LocalizedString> localizations = new();
        private TextMeshProUGUI bindText;
        private void Awake() => bindText = GetComponent<TextMeshProUGUI>();
        private void OnEnable()
        {
            GameManager.instance.OnCurrentLanguageChanged.AddListener(SetTranslatedText);
            SetTranslatedText();
        }
        private void OnDisable()
        {
            GameManager.instance.OnCurrentLanguageChanged.RemoveListener(SetTranslatedText);
        }
        public void SetTranslatedText()
        {
            foreach (LocalizedString localization in localizations)
            {
                if (localization.language == GameManager.instance.CurrentLanguage)
                {
                    bindText.text = localization.text;
                    break;
                }
            }
        }
    }
}
