using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class ParryBinding : MonoBehaviour
    {
        private EnemyDamageDealer bindDealer;
        private Enemy enemy;
        private void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
            bindDealer = GetComponent<EnemyDamageDealer>();
        }

        private void OnEnable()
        {
            PlayerParryState.OnPerfectParry += OnParry;
        }

        private void OnDisable()
        {
            PlayerParryState.OnPerfectParry -= OnParry;
        }

        public void OnParry()
        {
            enemy.fsm.ChangeState(FSM.EnemyStateType.Stun);
        }
    }
}
