using UnityEngine;

namespace ToyGame
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth;
        public float CurrentHealth;
        void Start()
        {
            SetHealth(MaxHealth);
        }

        public void SetHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void GainFull()
        {
            CurrentHealth = MaxHealth;
        }

        public virtual void LoseHealthByDealer(DamageDealer dealer)
        {
            CurrentHealth -= dealer.DamageValue;
            if (DeathCheck()) HandleDeath();
        }

        public void HealHealth(float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }

        public bool DeathCheck()
        {
            return CurrentHealth <= 0f;
        }

        public virtual void HandleDeath() => Debug.Log("Dead");
    }
}
