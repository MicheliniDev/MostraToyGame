using System.Collections;
using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class EnemyEffectsCollection : MonoBehaviour
    {
        private Enemy enemy;

        [SerializeField] private ParticleSystem damageParticles;
        [SerializeField] private GameObject damageSound;
        [SerializeField] private GameObject deathParticlesPrefab;
        [SerializeField] private GameObject deathSound;
        
        private DamageFlasher spriteFlasher;
        void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
            spriteFlasher = GetComponent<DamageFlasher>();
        }

        public void Start()
        {
            enemy.health.OnEnemyDamaged.AddListener(HandleDamageEffects);
            enemy.health.OnEnemyDeath.AddListener(HandleDeathEffects);
            PlayerParryState.OnParry += HandleParryEffects;
        }

        public void OnDestroy()
        {
            enemy.health.OnEnemyDamaged.RemoveListener(HandleDamageEffects);
            enemy.health.OnEnemyDeath.RemoveListener(HandleDeathEffects);
            PlayerParryState.OnParry -= HandleParryEffects;
        }

        private void HandleDamageEffects()
        {
            spriteFlasher.Flash();
            if (Player.instance.transform.position.x > enemy.transform.position.x && damageParticles.transform.rotation.y == 0f 
                || Player.instance.transform.position.x < enemy.transform.position.x && damageParticles.transform.rotation.y == 180f)
            {
                Quaternion rotation = damageParticles.transform.rotation;
                rotation.y = rotation.y == 0f ? 180f : 0f;
                damageParticles.transform.rotation = rotation;
            }
            damageParticles.Emit(30);
            damageSound.SetActive(true);
        }

        private void HandleDeathEffects()
        {
            Instantiate(deathParticlesPrefab, enemy.transform.position, Quaternion.identity);
            deathSound.SetActive(true);
        }

        private void HandleParryEffects()
        {
            spriteFlasher.Flash();
        }
    }
}
