using UnityEngine;
using ToyGame.Physics;  

namespace ToyGame.FSM
{
    public abstract class PlayerState : MonoBehaviour
    {
        protected Player player => GetComponentInParent<Player>();
        protected PlayerFSMController fsm => player.fsm;
        protected IAnimationPlayer animationPlayer => player as IAnimationPlayer;
        protected PlayerMover playerMover => player.playerMover;
        protected bool CanMove
        {
            get
            {
                return playerMover.CanMove;
            }
            set
            {
                playerMover.CanMove = value;
            }
        }
        protected bool canFlip
        {
            get
            {
                return player.CanFlip;
            }
            set
            {
                player.CanFlip = value;
            }
        }
        public abstract PlayerStateType StateType { get; }
        public virtual void OnStateEnter() { }
        public virtual void OnStateUpdate() { }
        public virtual void OnStateFixedUpdate() { }
        public virtual void OnStateExit() { }
        public virtual void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.CanMove)
                CanMove = true;
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.CanFlip)
                canFlip = true;
        }
    }
}
