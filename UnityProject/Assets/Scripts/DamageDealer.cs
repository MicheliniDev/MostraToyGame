using ToyGame.Physics;
using UnityEngine;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDealer : MonoBehaviour
    {
        public float DamageValue;
        private void Reset()
        {
            var collider = GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Health>(out var receiver))
            {
                receiver?.LoseHealthByDealer(this);
            }
        }
    }
}
