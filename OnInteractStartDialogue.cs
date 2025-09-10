using UnityEngine;
using UnityEngine.Events;

namespace ToyGame
{
    public class OnInteractStartDialogue : MonoBehaviour, IInteractable
    {
        [SerializeField] private SO_Dialogue dialogue;
        [SerializeField] private GameObject arrow;

        [SerializeField] private bool isStartTrigger;
        public UnityEvent OnDialogueOver;
        private void OnEnable()
        {
            DialogueManager.instance.OnDialogueEnd.AddListener(TriggerDialogueOver);
        }

        private void OnDisable()
        {
            DialogueManager.instance.OnDialogueEnd.RemoveListener(TriggerDialogueOver);
        }

        public void Interact()
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        private void TriggerDialogueOver() => OnDialogueOver?.Invoke();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isStartTrigger)
                Interact();
            
            if (arrow) arrow?.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (arrow) arrow?.SetActive(false);
        }
    }
}
