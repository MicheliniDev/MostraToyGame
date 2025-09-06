using System.Collections;
using ToyGame.FSM;
using UnityEngine;

namespace ToyGame.Physics
{
    public class EnemyMover : PhysicsMover
    {
        private Enemy enemy;
        private Vector2 movement;
        public Vector2 MovementVelocity;
        public Vector2 KnockbackVelocity;
        public Rigidbody2D rigidbody2d
        {
            get
            {
                return rb;
            }
        }
        private void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        private void FixedUpdate()
        {
            movement = MovementVelocity + KnockbackVelocity;
            rb.linearVelocity = movement;
        }

        public void ApplyMovement(Vector2 velocity)
        {
            movement = rb.position + (velocity * Time.fixedDeltaTime);
            rb.MovePosition(movement);
        }

        public override void ApplyAnimationMovement(Vector2 amount)
        {
            Vector2 delta;
            if (enemy.fsm.CurrentState is EnemyAttackState)
            {
                EnemyAttackState currentState = enemy.fsm.CurrentState as EnemyAttackState;
                delta = currentState.ModifyMovementDelta(amount);
            }
            else
            {
                delta = amount;
            }
            MovementVelocity = delta;
        }

        public override void ApplyKnockback(float force, Facings direction)
        {
            StopAllCoroutines();
            StartCoroutine(SetKnockback(force, direction));
        }

        private IEnumerator SetKnockback(float force, Facings direction)
        {
            float timeToStopPerUnit = 0.1f;
            float startX = force * (int)direction;
            float totalTime = Mathf.Abs(startX) * timeToStopPerUnit;
            float elapsedTime = 0f;

            while (elapsedTime < totalTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / totalTime;
                KnockbackVelocity.x = Mathf.Lerp(startX, 0f, t);
                yield return null;
            }

            KnockbackVelocity.x = 0f;
            yield return null;
        }
    }
}
