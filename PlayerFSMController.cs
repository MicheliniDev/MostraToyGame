using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerFSMController : MonoBehaviour
    {
        private Player player;
        [SerializeField] private InputReader inputReader;

        [field: SerializeField] public PlayerState CurrentState { get; private set; }
        public PlayerStateType CurrentStateType => CurrentState.StateType;
        public Dictionary<PlayerStateType, PlayerState> StateCollection = new();

        public float attackTimer;
        void Start()
        {
            player = GetComponent<Player>();

            foreach(var state in GetComponentsInChildren<PlayerState>())
            {
                StateCollection.Add(state.StateType, state);
            }
            ChangeState(PlayerStateType.Normal);
        }

        private void OnEnable()
        {
            inputReader.attackEvent += AttackStateChecks;
            inputReader.parryEvent += ParryStateChecks;

            PlayerHealth.OnPlayerDeath += GoToDeathState;
        }

        private void OnDisable()
        {
            inputReader.attackEvent -= AttackStateChecks;
            inputReader.parryEvent -= ParryStateChecks;

            PlayerHealth.OnPlayerDeath -= GoToDeathState;
        }

        void Update()
        {
            CurrentState.OnStateUpdate();
            /*if (CurrentStateType != PlayerStateType.Attack)
            {
                attackTimer += Time.time;
                PlayerAttackState attackState = (PlayerAttackState)StateCollection[PlayerStateType.Attack];
                if (attackTimer > 1f)
                {
                    attackState.attackCount = 0;
                }
            }*/
        }

        private void FixedUpdate()
        {
            CurrentState.OnStateFixedUpdate();
        }

        public void AttackStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Attack)) return;
            ChangeState(PlayerStateType.Attack);
        }

        public void ParryStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Parry)) return;
            ChangeState(PlayerStateType.Parry);
        }

        private void GoToDeathState()
        {
            ChangeState(PlayerStateType.Death);
        }

        public void ChangeState(PlayerStateType targetState)
        {
            if (CurrentState != null && CurrentStateType == targetState) return;

            CurrentState?.OnStateExit();
            CurrentState = StateCollection[targetState];
            CurrentState?.OnStateEnter();
        }
    }
}
