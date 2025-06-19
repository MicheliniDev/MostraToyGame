using System.Collections;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyDeadState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.Dead;

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Destroy(enemy.gameObject);
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
