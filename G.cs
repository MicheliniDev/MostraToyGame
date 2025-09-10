using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class G : MonoBehaviour
    {
        public void GoToMenu() => GameManager.instance.QuitGame();
    }
}
