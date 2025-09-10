using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelConnection connection;
        private SceneConnection connectionToMake;

        public bool CheckScene = false;

        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject != Player.instance.gameObject) return;
            if (CheckScene == true)
            {
                connectionToMake = connection.connections[0];
            }
            else 
            { 
                for (int i = 0; i < connection.connections.Count; i++)
                {
                    if (connection.connections[i].scene.SceneName == SceneManager.GetActiveScene().name)
                    {
                        continue;
                    }
                    connectionToMake = connection.connections[i];
                    break;
                }
            }
            GameManager.instance.LoadLevel(connectionToMake);
        }
    }
}
