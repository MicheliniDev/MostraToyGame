using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ToyGame
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Scriptable Objects/Dialogue")]
    public class SO_Dialogue : ScriptableObject
    {
        public List<CharacterDialogue> conversation;
    }

    [System.Serializable]
    public struct CharacterDialogue
    {
        public string characterName;
        public Facings position;

        public List<LocalizedString> localizedDialogue;
        public float typeSpeed;

        public string GetTranslatedText(LanguageType language)
        {
            foreach (LocalizedString localizedString in localizedDialogue)
            {
                if (localizedString.language == language)
                {
                    return localizedString.text;
                }
            }
            return "Translation missing!"; 
        }
    }
}
