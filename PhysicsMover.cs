using UnityEngine;
using System.Collections;

namespace ToyGame.Physics
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PhysicsMover : MonoBehaviour
    {
        private Rigidbody2D rb;

        [Header("Physics Mover Variables")]
        public Rigidbody2D.SlideMovement slideSettings;

        [SerializeField] private BoxCollider2D Collider;
        public bool isGrounded
        {
            get
            {
                float extraHeight = 0.15f;
                Vector2 start = Collider.bounds.center;
                Vector2 end = start + Vector2.down * (Collider.bounds.extents.y + extraHeight);

                return Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Ground"));
            }
        }
        public bool IsAirborne => !isGrounded;

        public Vector2 MovementVelocity;
        public Vector2 ExternalVelocity;
        public Vector2 FinalVelocity;

        public bool hasGravity = true;
        public float gravityValue = -9.81f;
        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Reset()
        {
            slideSettings.maxIterations = 5;
            slideSettings.surfaceSlideAngle = 45f;
            slideSettings.gravitySlipAngle = 60f;
            slideSettings.surfaceUp = Vector2.up;
            slideSettings.gravity = new Vector2(0f, hasGravity ? gravityValue : 0f);
        }

        public void ApplyFinalVelocity()
        {
            FinalVelocity = MovementVelocity + ExternalVelocity;

            rb.Slide(FinalVelocity, Time.fixedDeltaTime, slideSettings);
            rb.linearVelocityY = FinalVelocity.y;
        }

        public void ApplyMovementVelocity(Vector2 amount)
        {
            MovementVelocity = amount;
        }

        public void ApplyExternalVelocity(Vector2 amount)
        {
            ExternalVelocity += amount;
        }

        public void ApplyImpulseX(float amount, bool isOppositeDirection = true, IFacingFlippable entityDirection = null)
        {
            if (entityDirection != null)
            {
                amount = isOppositeDirection ? amount * -(int)entityDirection.CurrentFacing : amount * (int)entityDirection.CurrentFacing;
            }
            //ImpulseVelocity.x += amount;
            //StartCoroutine(FadeVelocityOverTime("X"));
        }

        public void ApplyImpulseY(float amount, bool isOppositeDirection = true, IFacingFlippable entityDirection = null)
        {
            if (entityDirection != null)
            {
                amount = isOppositeDirection ? amount * -(int)entityDirection.CurrentFacing : amount * (int)entityDirection.CurrentFacing;
            }
            //ImpulseVelocity.y += amount;
            //StartCoroutine(FadeVelocityOverTime("Y"));
        }

        /*public IEnumerator FadeVelocityOverTime(string axis)
        {
            float timeToStopPerUnit = 0.2f;
            float start = axis == "X" ? ImpulseVelocity.x : ImpulseVelocity.y;
            float totalTime = Mathf.Abs(start) * timeToStopPerUnit;
            float elapsedTime = 0f;

            while (elapsedTime < totalTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / totalTime;
                start = Mathf.Lerp(start, 0f, t);
                yield return null;
            }

            start = 0f;
        }*/
    }
}

