using UnityEngine;

namespace ToyGame
{
    public class EnemyDamageDealer : DamageDealer
    {
        [HideInInspector] public ParryDataBase parryData;
        public bool IsParried;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            IsParried = false;
            parryData = GetComponent<ParryDataBase>();
        }
    }
}
