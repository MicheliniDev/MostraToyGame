using System.Collections.Generic;
using System;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyEngageState : EnemyState
    {
        [Serializable]
        public class EngageMoveControl
        {
            public string animationToPlay;
            public float velocity;
            public float distanceToTrigger;
        }
        public override EnemyStateType StateType => EnemyStateType.Engaged;
        [SerializeField] private List<EngageMoveControl> engageBehaviors;
        //private EngageMoveControl currentBehavior;

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetTargetToPlayer();
            enemyMover.MovementVelocity = Vector2.zero;
            canFlip = true;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            fsm.AttackCheck();
            MoveTypeCheck();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public void MoveTypeCheck()
        {
            foreach (var behavior in engageBehaviors)
            {
                /*if (currentBehavior == behavior)
                    continue;
                */
                if (Mathf.Abs(enemy.GetHorizontalDistanceToPlayer()) < behavior.distanceToTrigger)
                {
                    animationPlayer.PlayAnimation(behavior.animationToPlay); 
                    enemy.GoToTarget(behavior.velocity);
                    //currentBehavior = behavior;
                    return;
                }
            }
        }
    }
}
