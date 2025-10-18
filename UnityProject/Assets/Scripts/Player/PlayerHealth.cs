using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ToyGame
{
    public class PlayerHealth : Health
    {
        public float maxHealth = 100f;
        public EnemyDamageDealer currentDealer;

        public UnityEvent OnPlayerDeath;
        public UnityEvent OnPlayerHurt;

        [SerializeField] private bool isInvincible;
        public bool IsInvincible => isInvincible;

        public override void Start()
        {
            return;
        }
        
        public void OnEnable()
        {
            MaxHealth = GameManager.instance.isEasyMode ? maxHealth * 15 : maxHealth;
            SetHealth(MaxHealth);
        }

        public override void LoseHealthByDealer(DamageDealer dealer)
        {
            if (isInvincible) return;

            if (dealer is EnemyDamageDealer)
            {
                currentDealer = dealer as EnemyDamageDealer;
                StartCoroutine(DealDamageIfNotParried(currentDealer));
            }
            else
            {
                base.LoseHealthByDealer(dealer);
            }
        }

        private IEnumerator DealDamageIfNotParried(EnemyDamageDealer dealer)
        {
            float timeDelay = 0f;
            while (timeDelay < 0.23f)
            {
                timeDelay += Time.deltaTime;
                if (dealer.IsParried)
                {
                    yield break;
                }
                yield return null;
            }
            base.LoseHealthByDealer(dealer);

            if (dealer.parryData is ParryDataProjectile)
            {
                var projectile = dealer.parryData as ParryDataProjectile;
                Destroy(projectile.transform.parent.gameObject);
            }
            ClearDealer();
            OnPlayerHurt?.Invoke();
            yield return null;
        }

        public void BecomeInvincible() => isInvincible = true;
        public void RemoveInvincible() => isInvincible = false;
        private void ClearDealer() => currentDealer = null;

        public override void LoseHealthByAmount(float amount)
        {
            if (isInvincible) return;
            base.LoseHealthByAmount(amount);
        }

        public override void HandleDeath()
        {
            base.HandleDeath();
            OnPlayerDeath?.Invoke();
        }
    }
}
