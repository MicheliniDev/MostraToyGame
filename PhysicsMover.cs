using UnityEngine;
using System.Collections;

namespace ToyGame.Physics
{
    public class PhysicsMover : MonoBehaviour
    {
        [SerializeField] private float groundCheckDistance;
        protected Rigidbody2D rb;
        public bool isGrounded
        {
            get
            {
                Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.yellow);
                return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, 1 << LayerMask.NameToLayer("Ground"));
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
            rb.linearVelocity = delta;
        }

        public virtual void ApplyKnockback(float force, Facings direction) {}
    }
}