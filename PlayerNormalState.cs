using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerNormalState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Normal;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanMove = true;
            canFlip = true; 
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
            HandleAnimations();
        }

        private void HandleAnimations()
        {
            if (playerMover.isGrounded)
            {
                if (playerMover.MovementVelocity.x != 0f) animationPlayer.PlayAnimation("Run");
                else animationPlayer.PlayAnimation("Idle");
            }
        }
    }
}
