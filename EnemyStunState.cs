using System.Collections;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyStunState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Stun;
        [SerializeField] private float stunDuration;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyMover.MovementVelocity.x = 0f;
            animationPlayer.PlayAnimation(bindingAnimation.name);
            StartCoroutine(WaitForStunDuration(stunDuration));
        }

        private IEnumerator WaitForStunDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            fsm.FallbackFromAttack();
        }
    }
}
