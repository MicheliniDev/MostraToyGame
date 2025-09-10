using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame.FSM
{
    public class PlayerDeadState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Death;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done)
            {
                fsm.ChangeState(PlayerStateType.Revival);
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanMove = false;
            canFlip = false;
            playerMover.Velocity = Vector2.zero;
            animationPlayer.PlayAnimation("Death");
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
