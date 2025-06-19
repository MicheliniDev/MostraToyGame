using UnityEngine;

namespace ToyGame
{
    public class EnemyDamageDealer : DamageDealer
    {
        public bool IsParried;
        void OnEnable()
        {
            IsParried = false;
        }
    }
}
