using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerNormalState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Normal;

        private string clipToPlay;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanMove = true;
            canFlip = true; 
            canParry = true;

            fsm.ParryState?.ResetParryIndex();
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

            fsm.AttackStateChecks();
            fsm.ParryStateChecks();
            fsm.HealStateChecks();
        }

        private void HandleAnimations()
        {
            if (!playerMover.isGrounded)
            {
                if (playerMover.Velocity.y > 0.1f) clipToPlay = "Jump";
                else clipToPlay = "Fall";
            }
            else
            {
                if (player.playerMover.MovementAxis != 0f) clipToPlay = "Run";
                else clipToPlay = "Idle";
            }

            animationPlayer.PlayAnimation(clipToPlay);
        }
    }
}
