using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace ToyGame
{
    public enum LanguageType { 
        PT_BR,
        EN_US
    }
    public class LanguageDropdown : MonoBehaviour
    {
        private TMP_Dropdown dropdown;
        private LanguageType currentLanguageType
        {
            get
            {
                return GameManager.instance.CurrentLanguageType;
            }
            set
            {
                GameManager.instance.CurrentLanguageType = value;
            }
        }

        void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            dropdown.ClearOptions();

            var options = new List<string>();
            foreach (var lang in System.Enum.GetValues(typeof(LanguageType)))
            {
                options.Add(lang.ToString());
            }
            dropdown.AddOptions(options);

            dropdown.value = (int)currentLanguageType;
            dropdown.RefreshShownValue();

            dropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private void OnLanguageChanged(int index)
        {
            currentLanguageType = (LanguageType)index;
            GameManager.instance.OnCurrentLanguageChanged?.Invoke();
        }
    }
}
