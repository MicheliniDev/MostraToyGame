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

        private bool isInvincible;
        void Awake()
        {
            MaxHealth = maxHealth;
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

        public IEnumerator DealDamageIfNotParried(EnemyDamageDealer dealer)
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

            if (dealer == null)
                yield break;

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
        public void LoseHealthByAmount(float amount)
        {
            if (isInvincible) return;

            CurrentHealth -= amount;
            if (DeathCheck())
                HandleDeath();
        }

        public override void HandleDeath()
        {
            base.HandleDeath();
            OnPlayerDeath?.Invoke();
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
            GUILayout.Label($"<color='white'><size=100>{CurrentHealth}</size></color>");
            GUILayout.EndArea();
        }
    }
}
