using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class MainMenuLogic : MonoBehaviour
    {
        public void GoToScene1()
        {
            SceneManager.LoadSceneAsync("CenaTest");
        }

        public void GoToScene2()
        {
            SceneManager.LoadSceneAsync("cens");
        }
    }
}
