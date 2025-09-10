using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyChangePhaseState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.BossChangePhase;
        public GameObject[] statesToEnableUponLeave;

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            if (tag == EnemyAnimationEvents.AnimationEvents.Done)
            {
                fsm.ChangeState(EnemyStateType.Engaged);
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            canFlip = false;
            enemyMover.StopAllCoroutines();
            enemyMover.KnockbackVelocity = Vector2.zero;
            enemyMover.MovementVelocity = Vector2.zero;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            foreach (var state in statesToEnableUponLeave)
            {
                state.SetActive(true);
            }
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
