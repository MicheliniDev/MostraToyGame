using UnityEngine;
using UnityEngine.UI;

namespace ToyGame
{
    public class HealthBarBehavior : MonoBehaviour
    {
        [SerializeField] private Health owner;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Transform healthBarPos;

        private void Start()
        {
            healthBar.maxValue = owner.MaxHealth;
            healthBar.value = owner.CurrentHealth;
        }

        private void Update()
        {
            if (healthBarPos == null) return;
            transform.position = healthBarPos.position;
        }
        private void OnEnable() => owner.OnHealthChanged += UpdateHealthBar;
        private void OnDisable() => owner.OnHealthChanged -= UpdateHealthBar;
        private void UpdateHealthBar() => healthBar.value = owner.CurrentHealth;
    }
}
