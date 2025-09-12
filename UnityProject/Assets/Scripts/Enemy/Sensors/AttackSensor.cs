using ToyGame.FSM;
using UnityEngine;
using System.Collections.Generic;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AttackSensor : MonoBehaviour
    {
        public List<EnemyStateType> starterMoves = new();
        [SerializeField] private float canAttackTimer;

        [HideInInspector] public bool isFirstTimePlayerInside = false;

        public float time;
        public bool canIncreaseTimer = true;        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Player.instance == null) return;
            
            if (collision.gameObject == Player.instance.gameObject) 
                isFirstTimePlayerInside = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Player.instance == null) return;
            if (collision.gameObject != Player.instance.gameObject ) return;
            
            if (canIncreaseTimer)
                time += Time.deltaTime;
        }

        public bool CanAttack()
        {
            if (isFirstTimePlayerInside)
            {
                isFirstTimePlayerInside = false;
                return true;
            }
            return time >= canAttackTimer;
        }
        
        public EnemyStateType FetchAttack()
        {
            int index = Random.Range(0, starterMoves.Count);
            return starterMoves[index];
        }

        public void DisableAttackTimerIncrease()
        {
            canIncreaseTimer = false;
        }

        public void EnableAttackTimerIncrease()
        {
            canIncreaseTimer = true;
        }

        public void ResetTimer()
        {
            time = 0f;
        }

        public void TriggerAutoEngage()
        {
            time = canAttackTimer;
        }
    }
}
