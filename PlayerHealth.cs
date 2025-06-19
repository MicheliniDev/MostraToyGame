using System;
using System.Collections;
using UnityEngine;

namespace ToyGame
{
    public class PlayerHealth : Health
    {
        [SerializeField] private SO_PlayerStats stats;
        public EnemyDamageDealer currentDealer;

        public static event Action OnPlayerDeath;
        void Awake()
        {
            MaxHealth = stats.MaxHealth;
        }

        public override void LoseHealthByDealer(DamageDealer dealer)
        {
            currentDealer = dealer as EnemyDamageDealer;
            StartCoroutine(DealDamageIfNotParried(currentDealer));
        }

        public IEnumerator DealDamageIfNotParried(EnemyDamageDealer dealer)
        {
            float timeDelay = 0f;
            while (timeDelay < 0.6f)
            {
                timeDelay += Time.deltaTime;
                if (dealer.IsParried)
                {
                    Debug.Log("Dealer was parried");
                    yield break;
                }
                yield return null;
            }
            base.LoseHealthByDealer(dealer);
            ClearDealer();
            yield return null;
        }

        private void ClearDealer() => currentDealer = null;
        public void LoseHealthByAmount(float amount) => CurrentHealth -= amount;
        public override void HandleDeath()
        {
            base.HandleDeath();
            OnPlayerDeath?.Invoke();
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
            GUILayout.Label($"<color='white'><size=40>{CurrentHealth}</size></color>");
            GUILayout.EndArea();
        }
    }
}
