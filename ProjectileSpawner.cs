using UnityEngine;

namespace ToyGame
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Enemy owner;
        private void OnEnable()
        {
            GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
            if (instance.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.owner = owner;
            }
        }
    }
}
