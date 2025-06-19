using UnityEngine;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EngageSensor : MonoBehaviour
    {
        public bool CanEngage = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            CanEngage = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            CanEngage = false;
        }
    }
}
