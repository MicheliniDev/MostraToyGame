using System;
using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerParryState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Parry;
        [SerializeField] private PlayerHealth receiver;

        private float ParryTimer = 0f;

        public static event Action OnPerfectParry;
        public static event Action OnImperfectParry;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            CanMove = false;
            canFlip = false;

            animationPlayer.PlayAnimation("TryDefend");

            playerMover.MovementVelocity.x = 0f;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            DetectParry();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            ParryTimer = 0f;
        }

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done) fsm.ChangeState(PlayerStateType.Normal);
        }

        public void DetectParry()
        {
            ParryTimer += Time.deltaTime;
            if (receiver.currentDealer != null)
            {
                if (ParryTimer < 0.3f) PerfectParry();
                else if (ParryTimer < 0.6f) ImperfectParry();
            }
        }

        public void PerfectParry()
        {
            receiver.currentDealer.IsParried = true;
            animationPlayer.PlayAnimation("Parry", true);
            
            receiver.currentDealer = null;
            OnPerfectParry?.Invoke();
        }

        public void ImperfectParry()
        {
            receiver.currentDealer.IsParried = true;
            animationPlayer.PlayAnimation("Parry", true);

            receiver.LoseHealthByAmount(receiver.currentDealer.DamageValue / 2f);
            receiver.currentDealer = null;
            OnImperfectParry?.Invoke();
        }
    }
}
