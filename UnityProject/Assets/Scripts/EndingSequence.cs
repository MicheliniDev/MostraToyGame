using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class EndingSequence : MonoBehaviour
    {
        [SerializeField] private GameObject endingCanvas;
        public void SetEndingSequence() => endingCanvas.SetActive(true);
        public void GoToMenu() => GameManager.instance.QuitGame();
    }
}
