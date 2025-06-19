using UnityEngine;

namespace ToyGame
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
    public class SO_PlayerStats : ScriptableObject
    {
        [Header("Walk")]
        public float RunSpeed = 8f;
        public float GroundAccelerationSpeed = 50f;
        public float GroundDecelerationSpeed = 100f;
        public float AirAccelerationSpeed = 50f;
        public float AirDecelerationSpeed = 50f;

        [Header("Combat")]
        public float MaxHealth = 100f;
        public float AttackDamage = 20f;
        public float CounterAttackDamage = 100f;
    }
}
