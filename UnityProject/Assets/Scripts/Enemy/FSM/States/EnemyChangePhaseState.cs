using UnityEngine;
using System.Collections;

namespace ToyGame.FSM
{
    public class EnemyChangePhaseState : EnemyState
    {
        public override EnemyStateType StateType => EnemyStateType.BossChangePhase;
        public GameObject[] statesToEnableUponLeave;
        public GameObject[] statesToDisableUponLeave;
        public float newHealth;
        public EnemyStateType exitState;
        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            if (tag == EnemyAnimationEvents.AnimationEvents.UnLock)
                fsm.LockFSM(false);

            if (tag == EnemyAnimationEvents.AnimationEvents.Done)
            {
                enemy.health.gameObject.SetActive(true);
                enemy.health.SetHealth(newHealth);
                enemy.health.isInvincible = false;
                fsm.ChangeState(exitState);
            }
        }

        public override void OnStateEnter()
        {
            enemy.health.isInvincible = true;
            animationPlayer.PlayAnimation(bindingAnimation.name);
            base.OnStateEnter();
            canFlip = false;
            enemyMover.StopAllCoroutines();
            enemyMover.KnockbackVelocity = Vector2.zero;
            enemyMover.MovementVelocity = Vector2.zero;
            StartCoroutine(Setup());
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private IEnumerator Setup()
        {
            foreach (var state in statesToDisableUponLeave)
            {
                state.SetActive(false);
                yield return null;
            }

            foreach (var state in statesToEnableUponLeave)
            {
                state.SetActive(true);
                yield return null;
            }
            enemy.attackSensor.starterMoves.Remove(EnemyStateType.Attack1);
            enemy.attackSensor.starterMoves.Remove(EnemyStateType.Attack2);
            enemy.attackSensor.starterMoves.Add(EnemyStateType.Attack4);
            enemy.attackSensor.starterMoves.Add(EnemyStateType.Attack5);
            enemy.attackSensor.starterMoves.Add(EnemyStateType.Attack6);
            enemy.attackSensor.canIncreaseTimer = true;
            yield return null;
            fsm.LockFSM(true);
            yield return null;
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
