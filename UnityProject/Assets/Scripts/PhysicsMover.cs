using UnityEngine;
using System.Collections;

namespace ToyGame.Physics
{
    public class PhysicsMover : MonoBehaviour
    {
        protected Rigidbody2D rb;
        public float groundCheckDistance;
        public bool isGrounded;
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