using System.Collections;
using UnityEngine;
using System;

namespace ToyGame.Physics
{
    public class PlayerMover : PhysicsMover
    {
        [Header("Movement")]
        public float RunSpeed = 8f;
        public float GroundAccelerationSpeed = 50f;
        public float GroundDecelerationSpeed = 100f;
        public float AirAccelerationSpeed = 50f;
        public float AirDecelerationSpeed = 50f;

        [Space] 
        [Header("Jump")] 
        public bool CanJump = true;
        public float jumpShortSpeed = 3f;   
        public float jumpSpeed = 6f;          
        private bool isJumping = false;
        private bool isJumpCanceling = false;
        [SerializeField, Range(0f, 0.5f)] 
        private float coyoteTime = .125f;
        private float coyoteTimer = 0f;
        
        public float MovementAxis;

        public Vector2 Velocity
        {
            get => rb.linearVelocity;
            set => rb.linearVelocity = value;
        }

        private IFacingFlippable flippable;

        public static event Action<Facings> OnPlayerFacingChange;
        private void Start()
        {
            flippable = GetComponent<Player>() as IFacingFlippable;
            canMove = true;
        }

        private void Update()
        {
            MovementAxis = InputManager.instance.GetAxis("Move");
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, 1 << LayerMask.NameToLayer("Ground"));
            
            if (InputManager.instance.GetActionDown("Jump") && (isGrounded || coyoteTimer <= coyoteTime))
            {
                isJumping = true;
                coyoteTimer = 999f;
            }
            if (InputManager.instance.GetActionUp("Jump") && !isGrounded)
            {
                isJumpCanceling = true;
            }

            if (!isGrounded && !isJumping)
                coyoteTimer += Time.deltaTime;
            else if (isGrounded)
                coyoteTimer = 0f;
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateJump();
        }

        private void UpdateJump()
        {
            if (!CanJump) return;
            if (isJumping)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                isJumping = false;
            }

            if (isJumpCanceling)
            {
                if (rb.linearVelocity.y > jumpShortSpeed)
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpShortSpeed);
                isJumpCanceling = false;
            }
        }

        private void UpdateMovement()
        {
            float velocity = rb.linearVelocityX;
            float targetSpeed = MovementAxis * RunSpeed;

            float acceleration;
            float deceleration;

            if (isGrounded)
            {
                acceleration = GroundAccelerationSpeed;    
                deceleration = GroundDecelerationSpeed;   
            }
            else
            {
                acceleration = AirAccelerationSpeed;    
                deceleration = AirDecelerationSpeed;   
            }

            bool isTurning = Mathf.Sign(velocity) != Mathf.Sign(targetSpeed) && velocity != 0f;
            bool isSlowingDown = Mathf.Abs(targetSpeed) < Mathf.Abs(velocity);

            float currentAccel = (isTurning || isSlowingDown) ? deceleration : acceleration;

            float maxDelta = currentAccel * Time.fixedDeltaTime;

            velocity = Mathf.MoveTowards(velocity, targetSpeed, maxDelta);
            
            if (canMove)
                rb.linearVelocityX = velocity;
            CheckFacing();
        }

        public override void ApplyKnockback(float force, Facings direction)
        {
            StopAllCoroutines();
            StartCoroutine(Knockback(force, direction));
        }

        private IEnumerator Knockback(float force, Facings direction)
        {
            float timeToStopPerUnit = 0.1f;
            float startX = force * (int)direction;
            float totalTime = Mathf.Abs(startX) * timeToStopPerUnit;
            float elapsedTime = 0f;

            while (elapsedTime < totalTime)
            {
                canMove = false;
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / totalTime;
                rb.linearVelocityX = Mathf.Lerp(startX, 0f, t);
                yield return null;
            }
            rb.linearVelocityX = 0f;
            canMove = true;
            yield return null;
        }

        private void CheckFacing()
        {
            if (MovementAxis > 0f && flippable.CurrentFacing == Facings.Left 
                || MovementAxis < 0f && flippable.CurrentFacing == Facings.Right)
            {
                flippable.Flip();
                OnPlayerFacingChange?.Invoke(flippable.CurrentFacing);
            }
        }
    }
}
