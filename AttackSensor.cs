using ToyGame.FSM;
using UnityEngine;
using System.Collections.Generic;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AttackSensor : MonoBehaviour
    {
        [SerializeField] private List<EnemyStateType> starterMoves = new();
        [SerializeField] private float canAttackTimer;
        public float time;
        private void OnTriggerStay2D(Collider2D collision) => time = Time.time;
        public bool CanAttack() => time > canAttackTimer;
        public EnemyStateType FetchAttack()
        {
            int index = Random.Range(0, starterMoves.Count);
            return starterMoves[index];
        }
    }
}
