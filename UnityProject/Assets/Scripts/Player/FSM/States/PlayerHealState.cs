using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerHealState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Heal;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done)
                fsm.ChangeState(PlayerStateType.Normal);

            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Cancel)
                player.health.GainFull();        
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanMove = false;
            canFlip = false;
            animationPlayer.PlayAnimation("Heal");
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
