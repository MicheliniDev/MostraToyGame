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
            canParry = true;

            PlayerParryState state = fsm.StateCollection[PlayerStateType.Parry] as PlayerParryState;
            state.ResetParryIndex();
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
            if (playerMover.isGrounded)
            {
                if (player.playerMover.MovementAxis != 0f) animationPlayer.PlayAnimation("Run");
                else animationPlayer.PlayAnimation("Idle");
            }
        }
    }
}
