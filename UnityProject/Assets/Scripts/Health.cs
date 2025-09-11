using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ToyGame
{
    public class Health : MonoBehaviour
    { 
        public float MaxHealth;
        public float CurrentHealth;

        public event Action OnHealthChanged;
        public event Action<DamageDealer> OnHealthChangedData;
        void Start()
        {
            SetHealth(MaxHealth);
        }

        public void SetHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            OnHealthChanged?.Invoke();
        }

        public void GainFull()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke();
        }

        public virtual void LoseHealthByDealer(DamageDealer dealer)
        {
            CurrentHealth -= dealer.DamageValue;
            if (DeathCheck()) HandleDeath();
            OnHealthChanged?.Invoke();
            OnHealthChangedData?.Invoke(dealer);
        }

        public void HealHealth(float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth) 
                CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke();
        }

        public bool DeathCheck() => CurrentHealth <= 0f;
        public virtual void HandleDeath() { }
    }
}
