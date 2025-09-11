using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ToyGame.FSM
{
    public class EnemyDeadState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Dead;

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            if (bindingAnimation != null)
                animationPlayer.PlayAnimation(bindingAnimation.name);
            yield return new WaitForSeconds(0.3f);
            enemy.effects.HandleDeathEffects();
            Destroy(enemy.gameObject);
        }
        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }
    }
}
