using ToyGame.Physics;

namespace ToyGame.FSM
{
    public class PlayerAttackState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Attack;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done)
            {
                fsm.ChangeState(PlayerStateType.Normal);
            }
        }

        private string animationClip;
        public int attackCount = 0;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            canFlip = false;

            if (playerMover.isGrounded) CanMove = false;
            else CanMove = true;

            switch (attackCount)
            {
                case 0: 
                    animationClip = "Attack0";
                    attackCount = 1;
                    break;
                case 1:
                    animationClip = "Attack1";
                    attackCount = 2;
                    break;
                case 2:
                    animationClip = "CounterAttack";
                    attackCount = 0;
                    break;
            }
            animationPlayer.PlayAnimation(animationClip);
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
