using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyWanderingState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Wandering;
        
        [SerializeField] private Transform[] wanderingPoints;
        [SerializeField] private float movementVelocity;
        private int currentIndex = 0;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            canFlip = true;
            SetWanderingPoint(currentIndex);
            enemy.attackCount = 0;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            fsm.EngageCheck();

            animationPlayer.PlayAnimation(bindingAnimation.name);
            
            if (CheckTargetReached())
                fsm.ChangeState(EnemyStateType.Idle);
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
            enemy.GoToTarget(movementVelocity);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            switch (currentIndex)
            {
                case 0:
                    currentIndex = 1;
                    break;
                case 1:
                    currentIndex = 0;
                    break;
            }
        }

        private void SetWanderingPoint(int index)
        {
            if (wanderingPoints.Length == 0 || wanderingPoints == null || enemy.CurrentTarget == null) return;

            enemy.CurrentTarget = wanderingPoints[index];
        }

        private bool CheckTargetReached()
        {
            if (enemy.CurrentTarget == null) 
            {
                return false;
            }
            return Mathf.Abs(enemy.CurrentTarget.position.x - enemy.transform.position.x) < 0.5f;
        }
    }
}