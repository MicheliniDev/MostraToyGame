using UnityEngine;

namespace ToyGame.Physics
{
    public class KnockbackReceiver : MonoBehaviour
    {
        private PhysicsMover bindMover;
        private Health health;
        private void Awake()
        {
            bindMover = GetComponentInParent<PhysicsMover>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            health.OnHealthChangedData += SetKnockback;
        }

        private void OnDestroy()
        {
            health.OnHealthChangedData -= SetKnockback;
        }

        private void SetKnockback(DamageDealer dealer)
        {
            if (dealer == null) return;

            if (dealer.TryGetComponent<KnockbackDealer>(out var KDealer))
            {
                bindMover.ApplyKnockback(KDealer.KnockbackAmount, KDealer.ownerFacing);
            }
        }
    }
}
