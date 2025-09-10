using UnityEngine;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerSensor : MonoBehaviour
    {
        public bool IsPlayerInside = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            IsPlayerInside = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsPlayerInside = false;
        }
    }
}
