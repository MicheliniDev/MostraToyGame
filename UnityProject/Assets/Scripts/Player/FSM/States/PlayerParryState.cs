using System;
using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerParryState : PlayerState
    {
        [SerializeField] private PlayerHealth receiver;
        public override PlayerStateType StateType => PlayerStateType.Parry;

        private float[] perfectParryWindows = new float[]
        {
            9 / 60f,
            4 / 60f,
            0f,
            0f,
            0f,
        };
        private int parryIndex;
        public float PerfectParryWindow;
        public float ImperfectParryWindow = 0.3f;

        private float ParryTimer = 0f;
        public bool wasPerfectParried;

        public static event Action OnPerfectParry;
        public static event Action OnImperfectParry;
        public static event Action OnParry;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            if (playerMover.isGrounded)
                CanMove = false;
            else
                CanMove = true;

            canFlip = false;
            wasPerfectParried = false;
            playerMover.Velocity = Vector2.zero;

            animationPlayer.PlayAnimation("TryDefend", true, 0f);

            parryIndex++;
            if (parryIndex > perfectParryWindows.Length - 1)
                parryIndex = perfectParryWindows.Length - 1;
            PerfectParryWindow = perfectParryWindows[parryIndex];
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            DetectParry();
            fsm.ParryStateChecks();
            fsm.CounterAttackStateChecks();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            ParryTimer = 0f;
            wasPerfectParried = false;
        }

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done) 
                fsm.ChangeState(PlayerStateType.Normal);
        }

        public void DetectParry()
        {
            ParryTimer += Time.deltaTime;
            if (receiver.currentDealer != null)
            {
                Parry();
                if (ParryTimer <= PerfectParryWindow) 
                    PerfectParry();
                else if (ParryTimer <= ImperfectParryWindow) 
                    ImperfectParry();
            }
        }

        public void Parry()
        {
            receiver.currentDealer.IsParried = true;
            OnParry?.Invoke();
        }

        public void PerfectParry()
        {
            wasPerfectParried = true;
            receiver.currentDealer.parryData.OnPerfectParry();
            receiver.currentDealer = null;
            OnPerfectParry?.Invoke();
            animationPlayer.PlayAnimation("Parry", true);
            TimeManager.instance.PauseTimeForDuration(10f / 60f);
        }

        public void ImperfectParry()
        {
            receiver.LoseHealthByAmount(receiver.currentDealer.DamageValue / 2f);
            receiver.currentDealer = null;
            animationPlayer.PlayAnimation("ImperfectParry", true);
            OnImperfectParry?.Invoke();
        }

        public void ResetParryIndex() => parryIndex = 0;
    }
}
