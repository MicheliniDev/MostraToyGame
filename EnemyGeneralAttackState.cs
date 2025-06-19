using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyGeneralAttackState : EnemyState
    {
        [SerializeField] private EnemyStateType state;
        [SerializeField] private MoveLinker bindLinker;
        public override EnemyStateType StateType => state;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            animationPlayer.PlayAnimation(bindingAnimation.name);
            enemy.attackSensor.time = 0f;
            enemy.attackCount++;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);

            if (tag == EnemyAnimationEvents.AnimationEvents.Done)
            {
                ChainMoveIfValid();
            }
        }

        private void ChainMoveIfValid()
        {
            if (bindLinker != null)
            {
                EnemyStateType? attack = bindLinker.LinkNextMove();
                if (attack == null)
                    fsm.FallbackFromAttack();
                else
                    fsm.ChangeState(attack.Value);
            }
        }
    }
}
