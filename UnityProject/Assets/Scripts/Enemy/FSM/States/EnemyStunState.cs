using System.Collections;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyStunState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Stun;
        public EnemyStateType exitState = EnemyStateType.None;
        [SerializeField] private float stunDuration;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyMover.StopAllCoroutines();
            enemyMover.MovementVelocity = Vector2.zero;
            enemyMover.KnockbackVelocity = Vector2.zero;
            animationPlayer.PlayAnimation(bindingAnimation.name);
            StartCoroutine(WaitForStunDuration(stunDuration));
        }

        private IEnumerator WaitForStunDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            if (exitState != EnemyStateType.None) fsm.ChangeState(exitState);
            else fsm.FallbackFromAttack();
            
            yield return null;
        }
    }
}
