using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyIdleState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Idle;

        private bool HasWandering => fsm.GetState(EnemyStateType.Wandering) != null;
        private float idleTimer;
        [SerializeField] private float idleTime;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            canFlip = true;
            enemyMover.rigidbody2d.linearVelocityX = 0f;
            idleTimer = 0f;
            enemy.attackCount = 0;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            animationPlayer.PlayAnimation(bindingAnimation.name);

            fsm.EngageCheck();

            idleTimer += Time.deltaTime;
            if (HasWandering && idleTimer > idleTime) 
                fsm.ChangeState(EnemyStateType.Wandering);
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            idleTimer = 0f;
        }
    }
}
