using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class OnInteractCheckpointSetter : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject arrow;
        public void Interact()
        {
            Player.instance.SetCheckPoint(transform);
            Player.instance.health.GainFull();
            SceneManager.LoadSceneAsync(Player.instance.Checkpoint.scene.name);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            arrow.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            arrow.SetActive(false);
        }
    }
}
