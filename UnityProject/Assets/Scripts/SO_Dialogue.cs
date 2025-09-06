using NUnit.Framework;
using System.Collections.Generic;
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
        public Sprite characterSprite;

        [TextArea] public string dialogue;
        public float typeSpeed;
    }
}
