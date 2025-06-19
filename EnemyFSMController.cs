using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyFSMController : MonoBehaviour
    {
        private Enemy enemy;
        [field: SerializeField] public EnemyState CurrentState { get; private set; }
        public EnemyStateType CurrentStateType => CurrentState.StateType;
        public Dictionary<EnemyStateType, EnemyState> StateCollection = new();
        void Start()
        {
            enemy = GetComponentInParent<Enemy>();  

            foreach (var state in GetComponentsInChildren<EnemyState>())
            {
                StateCollection.Add(state.StateType, state);
            }
            ChangeState(EnemyStateType.Idle);
        }

        void Update()
        {
            CurrentState?.OnStateUpdate();
            if (Input.GetKeyDown(KeyCode.G)) ChangeState(EnemyStateType.Attack1);
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate();
        }

        public void EngageCheck()
        {
            if (enemy.engageSensor == null || StateCollection[EnemyStateType.Engaged] == null) return;
            if (enemy.engageSensor.CanEngage)
                ChangeState(EnemyStateType.Engaged);
        }

        public void AttackCheck()
        {
            if (enemy.attackSensor == null) return;
            
            if (enemy.attackSensor.CanAttack())
                ChangeState(enemy.attackSensor.FetchAttack());
        }

        public void FallbackFromAttack()
        {
            if (enemy.engageSensor.CanEngage && CurrentStateType != EnemyStateType.Engaged)
            {
                ChangeState(EnemyStateType.Engaged);
                Debug.Log($"Fallback from Attack: Engaged");
                StartCoroutine(ResetAttackCountAfterTimer(3));
            }
            else if (GetState(EnemyStateType.Wandering) != null && CurrentStateType != EnemyStateType.Wandering)
            {
                ChangeState(EnemyStateType.Wandering);
                Debug.Log($"Fallback from Attack: Wandering");
            }
            {
                ChangeState(EnemyStateType.Idle);
                Debug.Log($"Fallback from Attack: Idle");
            }
        }

        private IEnumerator ResetAttackCountAfterTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            enemy.attackCount = 0;
        }

        public void ChangeState(EnemyStateType targetState)
        {
            if (CurrentState != null && CurrentStateType == targetState) return;

            CurrentState?.OnStateExit();
            CurrentState = StateCollection[targetState];
            CurrentState?.OnStateEnter();
        }

        public EnemyState GetState(EnemyStateType state)
        {
            if (StateCollection.ContainsKey(state) == false) {
                return null;
            }
            else return StateCollection[state];
        }
    }
}
