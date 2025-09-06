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
        [HideInInspector] public EnemyHealth health;
        [HideInInspector] public PlayerSensor engageSensor;
        [HideInInspector] public AttackSensor attackSensor;

        public Transform CurrentTarget;
        
        public int maxAttackCount;
        public int attackCount;

        void Awake()
        {
            mover = GetComponent<EnemyMover>();
            fsm = GetComponent<EnemyFSMController>();
            enemyMover = GetComponent<EnemyMover>();
            health = GetComponentInChildren<EnemyHealth>();
            engageSensor = GetComponentInChildren<PlayerSensor>();
            attackSensor = GetComponentInChildren<AttackSensor>();
        }

        private void Update()
        {
            FlipCheck();
        }

        public void SetTargetToPlayer()
        {
            if (Player.instance == null) return;
            CurrentTarget = Player.instance.transform;
        }

        public float GetHorizontalDistanceToPlayer()
        {
            if (Player.instance == null) 
                return 9999f;
            return Player.instance.transform.position.x - transform.position.x;
        }

        public void GoToTarget(float movementVelocity)
        {
            Vector2 direction = (CurrentTarget.position - transform.position).normalized;
            //enemyMover.ApplyMovement(new Vector2(direction.x * movementVelocity, 0f));
            enemyMover.MovementVelocity.x = direction.x * movementVelocity; 
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
