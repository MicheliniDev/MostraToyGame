using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ToyGame.FSM
{
    public class EnemyFSMController : MonoBehaviour
    {
        private Enemy enemy;
        [field: SerializeField] public EnemyState CurrentState { get; private set; }
        public EnemyStateType CurrentStateType => CurrentState?.StateType ?? EnemyStateType.Idle;
        public Dictionary<EnemyStateType, EnemyState> StateCollection = new();

        [SerializeField] private EnemyStateType startingState;
        [SerializeField] private List<Enemy> enemiesToForceEngage; 

        public UnityEvent OnEnemyEngage;
        void Start()
        {
            enemy = GetComponentInParent<Enemy>();  

            foreach (var state in GetComponentsInChildren<EnemyState>())
            {
                StateCollection.Add(state.StateType, state);
            }
            CurrentState = StateCollection[startingState];
            CurrentState.OnStateEnter();
        }

        void Update()
        {
            CurrentState?.OnStateUpdate();
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate();
        }

        public void EngageCheck()
        {
            if (enemy.engageSensor == null || StateCollection[EnemyStateType.Engaged] == null) return;
            if (enemy.engageSensor.IsPlayerInside)
            {
                ChangeState(EnemyStateType.Engaged);
                OnEnemyEngage?.Invoke();
                foreach (var enemy in enemiesToForceEngage)
                {
                    enemy.fsm.ForceEngageIfValid();
                }
            }
        }

        public void ForceEngageIfValid()
        {
            if (CurrentStateType == EnemyStateType.Idle || CurrentStateType == EnemyStateType.Wandering)
                ForceEngageAfterDelay();
        }

        private IEnumerator ForceEngageAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            ChangeState(EnemyStateType.Engaged);
        }

        public bool AttackCheck()
        {
            if (enemy.attackSensor == null) 
                return false;
            
            if (enemy.attackSensor.CanAttack())
            {
                enemy.attackSensor.DisableAttackTimerIncrease();

                EnemyTransitionToAttackState transition = GetState(EnemyStateType.TransitionToAttack) as EnemyTransitionToAttackState;
                transition.AttackState = enemy.attackSensor.FetchAttack();
                ChangeState(EnemyStateType.TransitionToAttack);
            }
            return true;
        }

        public void FallbackFromAttack()
        {
            enemy.attackSensor.EnableAttackTimerIncrease();
            enemy.attackSensor.ResetTimer();
            enemy.attackCount = 0;

            if (enemy.engageSensor.IsPlayerInside)
                ChangeState(EnemyStateType.Engaged);
            else
            {
                ChangeState(startingState);
            }
        }

        public void ChangeState(EnemyStateType targetState)
        {
            CurrentState?.OnStateExit();
            CurrentState = StateCollection[targetState];
            CurrentState?.OnStateEnter();
        }

        public EnemyState GetState(EnemyStateType state)
        {
            if (!StateCollection.ContainsKey(state)) {
                return null;
            }
            else return StateCollection[state];
        }

        public void AddToStateMachine(EnemyStateType stateType, EnemyState state)
        {
            StateCollection.Add(stateType, state);
        }
    }
}
