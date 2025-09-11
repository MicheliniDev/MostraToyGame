using UnityEngine;
using UnityEngine.Pool;

namespace ToyGame
{
    public class Projectile : MonoBehaviour, IFacingFlippable
    {
        public Facings CurrentFacing { get; set; }
        public bool CanFlip { get; set; }
        [SerializeField] private Facings StartFacing;

        private Rigidbody2D rb;
        public Enemy owner;
        public float movementSpeed;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            CurrentFacing = StartFacing;

            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            Vector2 direction = (Player.instance.transform.position - transform.position).normalized;
            SetDirectionAndVelocity(direction, movementSpeed);
            RotateInDirection(direction);
        }

        public void ReflectTowardsOwner()
        {
            Vector2 direction = (owner.transform.position - transform.position).normalized;
            SetDirectionAndVelocity(direction, movementSpeed);
            RotateInDirection(direction);
        }

        private void SetDirectionAndVelocity(Vector2 direction, float velocity)
        {
            rb.linearVelocity = new Vector2(direction.x, direction.y) * velocity;
        }

        public void RotateInDirection(Vector2 dir)
        {
            float angle = Vector2.SignedAngle(Vector2.right, dir);
            if (dir.x < 0f)
                angle += 180f;

            if (dir.x < 0f && CurrentFacing == Facings.Right || dir.x > 0f && CurrentFacing == Facings.Left)
            {
                IFacingFlippable thi = this as IFacingFlippable;
                thi.Flip();
                angle += 180f;
            }
            transform.localEulerAngles = new Vector3(0f, 0f, angle);
        }
    }
}