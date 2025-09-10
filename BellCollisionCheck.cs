using UnityEngine;

namespace ToyGame
{
    public class BellCollisionCheck : MonoBehaviour
    {
        public GameObject levelLoader;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            levelLoader.SetActive(true);   
        }
    }
}
