using UnityEngine;
using System.Collections;

namespace ToyGame.Physics
{
    public class PhysicsMover : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private Transform groundCheck;
        protected Rigidbody2D rb;
        public bool isGrounded
        {
            get
            {
                if (groundCheck == null) 
                    return false;
                return Physics2D.OverlapCircle(groundCheck.position, 0.3f, 1 << LayerMask.NameToLayer("Ground"));
            }
        }
        public bool IsAirborne => !isGrounded;
        public bool canMove;
        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void ApplyAnimationMovement(Vector2 delta)
        {
            /*Vector2 movement = rb.position + delta * Time.fixedDeltaTime;
            rb.MovePosition(movement);*/
            rb.linearVelocity = delta;
        }

        public virtual void ApplyKnockback(float force, Facings direction) {}

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
        }
    }
}