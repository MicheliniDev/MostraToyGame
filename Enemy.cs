using ToyGame.Physics;
using ToyGame.FSM;
using UnityEngine;
using JetBrains.Annotations;

namespace ToyGame
{
    public class Enemy : MonoBehaviour, IFacingFlippable, IAnimationPlayer
    {
        [field:SerializeField] public Facings CurrentFacing { get; set; }
        public bool CanFlip { get ; set ; }
        public Animator anim => GetComponentInChildren<Animator>();
        public IFacingFlippable flipper => this as IFacingFlippable;

        [HideInInspector] public EnemyMover mover;
        [HideInInspector] public EnemyFSMController fsm;
        [HideInInspector] public EnemyMover enemyMover;
        [HideInInspector] public EngageSensor engageSensor;
        [HideInInspector] public AttackSensor attackSensor;

        public Transform CurrentTarget;

        private GameObject player;
        
        public int maxAttackCount;
        public int attackCount;
        void Awake()
        {
            mover = GetComponent<EnemyMover>();
            fsm = GetComponent<EnemyFSMController>();
            enemyMover = GetComponent<EnemyMover>();
            engageSensor = GetComponentInChildren<EngageSensor>();
            attackSensor = GetComponentInChildren<AttackSensor>();

            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            FlipCheck();
        }

        public void SetTargetToPlayer()
        {
            CurrentTarget = player.transform;
        }

        public float GetHorizontalDistanceToPlayer()
        {
            return player.transform.position.x - transform.position.x;
        }

        public void GoToTarget(float X, float movementVelocity)
        {
            enemyMover.ApplyMovementVelocity(new Vector2(X * movementVelocity, enemyMover.MovementVelocity.y));
        }

        private void FlipCheck()
        {
            if (CurrentTarget == null || CanFlip == false) return;

            float distance = CurrentTarget.transform.position.x - transform.position.x;
            if (distance > 0f && CurrentFacing == Facings.Left || distance < 0f && CurrentFacing == Facings.Right)
            {
                flipper.Flip();
            }
        }
    }
}
