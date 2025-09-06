using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class ParryDataEnemyAttack : ParryDataBase
    {
        public Enemy enemy;
        public EnemyStateType stunState;
        public bool isStunOnPerfectParry;

        private void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
        }

        public override void OnPerfectParry()
        {
            if (isStunOnPerfectParry)
            {
                enemy.fsm.ChangeState(stunState);
            }
        }
    }
}
