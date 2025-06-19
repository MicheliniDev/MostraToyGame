using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyEngageState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Engaged;
        [SerializeField] private float RunSpeed;
        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetTargetToPlayer();
            enemy.enemyMover.MovementVelocity.x = 0f;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            fsm.AttackCheck();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
            enemy.GoToTarget(enemy.GetHorizontalDistanceToPlayer(), RunSpeed);
            animationPlayer.PlayAnimation(bindingAnimation.name);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}
