using UnityEngine;

namespace ToyGame.FSM
{
    public enum AttackMoveType
    {
        Normal,
        ToPlayer
    }

    public class EnemyAttackState : EnemyState
    {
        [SerializeField] private EnemyStateType state;
        [SerializeField] private MoveLinker bindLinker;
        public override EnemyStateType StateType => state;

        public AttackMoveType moveType;
        private float radiansToPlayer;
        private Vector2 distanceToPlayer;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            canFlip = true;
            enemyMover.MovementVelocity = Vector2.zero;
            animationPlayer.PlayAnimation(bindingAnimation.name, true);
            enemy.attackCount++;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            distanceToPlayer = Player.instance.transform.position - enemy.transform.position;
            radiansToPlayer = Mathf.Atan2(distanceToPlayer.y, distanceToPlayer.x);
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnAnimationEvent(EnemyAnimationEvents.AnimationEvents tag)
        {
            base.OnAnimationEvent(tag);

            if (tag == EnemyAnimationEvents.AnimationEvents.Done)
            {
                ChainMoveIfValid();
            }
        }

        private void ChainMoveIfValid()
        {
            if (bindLinker == null)
            {
                fsm.FallbackFromAttack();
                return;
            }
            
            EnemyStateType? attack = bindLinker.LinkNextMove();
            if (attack == null)
            {
                fsm.FallbackFromAttack();
            }
            else
            {
                EnemyState attackComponent = fsm.GetState(attack.Value);
                if (attackComponent != null)
                    fsm.ChangeState(attack.Value);
                else 
                    fsm.FallbackFromAttack();
            }
        }

        public Vector2 ModifyMovementDelta(Vector2 amount)
        {
            if (moveType == AttackMoveType.Normal)
            {
                return amount;
            }

            Vector2 originalAmount = amount;
            Vector3 vector = amount;

            amount = Quaternion.Euler(0f, 0f, radiansToPlayer * 57.29578f) * vector;

            if (amount.y != 0f && originalAmount.y == 0f)
                amount.y = 0f;

            float threshold = 3f;
            if (Mathf.Abs(distanceToPlayer.x) < threshold)
                amount = (amount.normalized * distanceToPlayer) / 2f;

            if (enemy.CurrentFacing == Facings.Left)
                amount = -amount;

            if (amount.x > 0f && enemy.CurrentFacing == Facings.Left && originalAmount.x < 0f ||
                amount.x < 0f && enemy.CurrentFacing == Facings.Right && originalAmount.x > 0f)
                amount.x = 0f;

            return amount;
        }
    }
}
