using UnityEngine;

namespace ToyGame
{
    public class EnemyDamageDealer : DamageDealer
    {
        [HideInInspector] public ParryDataBase parryData;
        public bool IsParried;
        
        public void OnEnable()
        {
            IsParried = false;
            parryData = GetComponent<ParryDataBase>();
        }
    }
}
