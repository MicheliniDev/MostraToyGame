using System.Collections;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyTransitionToAttackState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.TransitionToAttack;
        public EnemyStateType AttackState;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StartCoroutine(GoToAttackAfterDelay());
        }

        private IEnumerator GoToAttackAfterDelay()
        {
            yield return null;
            fsm.ChangeState(AttackState);
        }
    }
}
