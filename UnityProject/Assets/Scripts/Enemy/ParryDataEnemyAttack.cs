using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class ParryDataEnemyAttack : ParryDataBase
    {
        public Enemy enemy;
        public EnemyStateType stunState;
        public bool isStunOnPerfectParry;
        private DamageFlasher flash;

        private void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
            flash = enemy.GetComponentInChildren<DamageFlasher>();
        }

        public override void OnPerfectParry()
        {
            flash.Flash();
            if (isStunOnPerfectParry)
            {
                if (enemy.fsm.GetState(stunState))
                    enemy.fsm.ChangeState(stunState);
            }
        }
    }
}
