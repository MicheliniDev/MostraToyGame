using ToyGame.Physics;
using UnityEngine;

namespace ToyGame.FSM
{
    public abstract class EnemyState : MonoBehaviour
    {
        protected Enemy enemy => GetComponentInParent<Enemy>();
        protected EnemyFSMController fsm => enemy.fsm;
        protected IAnimationPlayer animationPlayer => enemy as IAnimationPlayer;
        protected EnemyMover enemyMover => enemy.enemyMover;
        protected bool canFlip
        {
            get
            {
                return enemy.CanFlip;
            }
            set
            {
                enemy.CanFlip = value;
            }
        }
        public AnimationClip bindingAnimation;
        public abstract EnemyStateType StateType { get; }
        public virtual void OnStateEnter() { }
        public virtual void OnStateUpdate() { }
        public virtual void OnStateFixedUpdate() { }
        public virtual void OnStateExit() { }
        public virtual void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            if (tag == EnemyAnimationEvents.AnimationEvents.StopFlipCheck) canFlip = false;
        }

        private void OnDisable()
        {
            fsm.Remove(StateType);
        }
    }
}
