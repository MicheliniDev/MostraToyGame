using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ToyGame
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;

        private SO_Dialogue currentConversation;
        private int currentDialogueIndex;

        public GameObject DialogueWrapper;
        public GameObject ContinueDialogueArrow;
        public TextMeshProUGUI DialogueText;

        public Image DialogueCharacterImageLeft;
        public Image DialogueCharacterImageRight;
        public TextMeshProUGUI DialogueCharacterNameLeft;
        public TextMeshProUGUI DialogueCharacterNameRight;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;
        }

        private void Update()
        {
            if (InputManager.instance.GetActionDown("AdvanceDialogue") && currentConversation != null)
            {
                AdvanceDialogue();
            }    
        }

        public void StartDialogue(SO_Dialogue dialogue)
        {
            if (currentConversation == dialogue)
            {
                Debug.Log("Dialogue is already playing");
                return;
            }
            InputManager.instance.SwitchCurrentActionMap(InputMap.Dialogue);
            Player.instance.health.BecomeInvincible();
            currentConversation = dialogue;

            DialogueWrapper.SetActive(true);
            SetCharacterSpriteAndName(currentConversation);

            StartCoroutine(TypeDialogue(currentConversation));
        }

        public void AdvanceDialogue()
        {
            if (DialogueText.text != currentConversation.conversation[currentDialogueIndex].dialogue)
            {
                StopAllCoroutines();
                DialogueText.text = currentConversation.conversation[currentDialogueIndex].dialogue;
                ContinueDialogueArrow.SetActive(true);
                return;
            }

            ContinueDialogueArrow.SetActive(false);
            if (currentDialogueIndex == currentConversation.conversation.Count - 1)
            {
                EndDialogue();
                return;
            }

            DialogueText.text = string.Empty;
            currentDialogueIndex++;
            SetCharacterSpriteAndName(currentConversation);
            StartCoroutine(TypeDialogue(currentConversation));
        }

        private IEnumerator TypeDialogue(SO_Dialogue dialogue)
        {
            var currentConversation = dialogue.conversation[currentDialogueIndex];
            var currentDialogueArray = currentConversation.dialogue.ToCharArray();
            for (int i = 0; i < currentDialogueArray.Length; i++)
            {
                DialogueText.text += currentDialogueArray[i];
                yield return new WaitForSeconds(currentConversation.typeSpeed);
            }
            ContinueDialogueArrow.SetActive(true);
            yield return null;
        }

        public void SetCharacterSpriteAndName(SO_Dialogue conversation)
        {
            var emptyColor = new Color(1f, 1f, 1f, 0f);
            var fullColor = new Color(1f, 1f, 1f, 1f);
            var current = conversation.conversation[currentDialogueIndex];
            switch (current.position) { 
                case Facings.Left:
                    DialogueCharacterImageRight.sprite = null;
                    DialogueCharacterImageRight.color = emptyColor;
                    DialogueCharacterNameRight.text = string.Empty;

                    DialogueCharacterImageLeft.sprite = current.characterSprite;
                    DialogueCharacterImageLeft.color = fullColor;
                    DialogueCharacterNameLeft.text = current.characterName;
                    break;
                case Facings.Right:
                    DialogueCharacterImageLeft.sprite = null;
                    DialogueCharacterImageLeft.color = emptyColor;
                    DialogueCharacterNameLeft.text = string.Empty;

                    DialogueCharacterImageRight.sprite = current.characterSprite;
                    DialogueCharacterImageRight.color = fullColor;
                    DialogueCharacterNameRight.text = current.characterName;
                    break;
            }
        }

        public void EndDialogue()
        {
            currentConversation = null;
            DialogueText.text = string.Empty;
            DialogueWrapper.SetActive(false);
            InputManager.instance.SwitchCurrentActionMap(InputMap.Gameplay);
            Player.instance.health.RemoveInvincible();
            currentDialogueIndex = 0;
        }
    }
}
