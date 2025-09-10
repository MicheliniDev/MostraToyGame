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
        public bool isCounterAttack;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            canParry = false;
            canFlip = false;
            fsm.attackTimer = 0f;

            if (playerMover.isGrounded) CanMove = false;
            else CanMove = true;

            if (!isCounterAttack)
            {
                if (playerMover.isGrounded)
                {
                    switch (attackCount)
                    {
                        case 0:
                            animationClip = "Attack0";
                            attackCount = 1;
                            break;
                        case 1:
                            animationClip = "Attack1";
                            attackCount = 0;
                            break;
                    }
                }
                else
                {
                    animationClip = "AirAttack";
                    attackCount = 0;
                }
            }
            else
            {
                animationClip = "CounterAttack";
                attackCount = 0;
            }
            animationPlayer.PlayAnimation(animationClip, true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            isCounterAttack = false;
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
