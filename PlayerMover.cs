using UnityEngine;

namespace ToyGame.Physics
{
    public class PlayerMover : PhysicsMover
    {
        [Header("Player Mover Variables")]
        [SerializeField] private InputReader input;
        [SerializeField] private SO_PlayerStats stats;

        public Vector2 MovementAxis;

        private IFacingFlippable flippable;

        public bool CanMove;
        private void Start()
        {
            flippable = GetComponent<Player>() as IFacingFlippable;
            CanMove = true;
        }

        private void OnEnable()
        {
            input.moveEvent += GetMovementAxis;
            //input.jumpEvent += Jump;
            //input.jumpCanceledEvent += CancelJump;
        }

        private void OnDisable()
        {
            input.moveEvent -= GetMovementAxis;
            //input.jumpEvent -= Jump;
            //input.jumpCanceledEvent -= CancelJump;
        }

        private void FixedUpdate() => UpdateMovement();

        public void GetMovementAxis(Vector2 axis) => MovementAxis = axis.normalized;

        public void UpdateMovement()
        {
            float velocity = MovementVelocity.x;
            float targetSpeed = MovementAxis.x * stats.RunSpeed;

            float acceleration;
            float deceleration;

            if (isGrounded)
            {
                acceleration = stats.GroundAccelerationSpeed;    // speeding up
                deceleration = stats.GroundDecelerationSpeed;   // slowing down / turning around
            }
            else
            {
                acceleration = stats.AirAccelerationSpeed;    // speeding up
                deceleration = stats.AirDecelerationSpeed;   // slowing down / turning around
            }

            bool isTurning = Mathf.Sign(velocity) != Mathf.Sign(targetSpeed) && velocity != 0f;
            bool isSlowingDown = Mathf.Abs(targetSpeed) < Mathf.Abs(velocity);

            float currentAccel = (isTurning || isSlowingDown) ? deceleration : acceleration;

            float maxDelta = currentAccel * Time.fixedDeltaTime;

            velocity = Mathf.MoveTowards(velocity, targetSpeed, maxDelta);

            if (CanMove)
                ApplyMovementVelocity(new Vector2(velocity, MovementVelocity.y));

            ApplyFinalVelocity();
            CheckFacing();
        }

        private void Jump()
        {
            if (!isGrounded) return;
            ApplyMovementVelocity(new Vector2(MovementVelocity.x, 10f));
        }

        private void CancelJump()
        {
            ApplyMovementVelocity(new Vector2(MovementVelocity.x, 0f));
        }

        private void CheckFacing()
        {
            if (MovementVelocity.x > 0f && flippable.CurrentFacing == Facings.Left
                || MovementVelocity.x < 0f && flippable.CurrentFacing == Facings.Right)
            {
                flippable.Flip();
            }
        }
    }
}
