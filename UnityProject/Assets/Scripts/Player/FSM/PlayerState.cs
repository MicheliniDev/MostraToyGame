using UnityEngine;
using ToyGame.Physics;  

namespace ToyGame.FSM
{
    public abstract class PlayerState : MonoBehaviour
    //classe do player
    {
        protected Player player => GetComponentInParent<Player>();
        //instancia da classe player recebe o componente do objeto, chamado player
        protected PlayerFSMController fsm => player.fsm;
        //
        protected IAnimationPlayer animationPlayer => player as IAnimationPlayer;
        protected PlayerMover playerMover => player.playerMover;
        protected bool CanMove
        {
            get
            {
                return playerMover.canMove;
            }
            set
            {
                playerMover.canMove = value;
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
        protected bool canParry
        {
            get
            {
                return player.canParry;
            }
            set
            {
                player.canParry = value;
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
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.CanParry)
                canParry = true;
        }
    }
}
