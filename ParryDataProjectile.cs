using UnityEngine;

namespace ToyGame
{
    public class ParryDataProjectile : ParryDataBase
    {
        [SerializeField] private bool isReturnToOwnerOnParry;
        public Projectile projectile;        
        private void Awake()
        {
            projectile = GetComponentInParent<Projectile>();
        }

        public override void OnPerfectParry()
        {
            if (isReturnToOwnerOnParry)
                projectile.ReflectTowardsOwner();
            else
                Destroy(projectile.gameObject);
        }
    }
}
