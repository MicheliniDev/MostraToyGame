using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerHurtState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Hurt;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done)
            {
                fsm.ChangeState(PlayerStateType.Normal);
            }    
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            animationPlayer.PlayAnimation("Hurt");
            canParry = false;
            CanMove = false;
            canFlip = false;
            TimeManager.instance.PauseTimeForDuration(10f / 60f);
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
