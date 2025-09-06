using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ToyGame.FSM
{
    public class PlayerFSMController : MonoBehaviour
    {
        public Dictionary<PlayerStateType, PlayerState> StateCollection = new();
        [field: SerializeField] public PlayerState CurrentState { get; private set; }
        public PlayerStateType CurrentStateType => CurrentState?.StateType ?? PlayerStateType.Normal;

        private PlayerAttackState attackState;
        private Player player;
        
        public float attackTimer;
        void Start()
        {
            player = GetComponent<Player>();

            foreach(var state in GetComponentsInChildren<PlayerState>())
            {
                StateCollection.Add(state.StateType, state);
            }
            CurrentState = StateCollection[PlayerStateType.Normal];
            CurrentState?.OnStateEnter();
            
            attackState = (PlayerAttackState)StateCollection[PlayerStateType.Attack];
        }

        void Update()
        {
            CurrentState?.OnStateUpdate();
            
            if (CurrentStateType != PlayerStateType.Attack)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > 1f)
                {
                    attackState.attackCount = 0;
                }
            }
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate();
        }

        public void AttackStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Attack)) return;
            
            if (InputManager.instance.GetActionDown("Attack"))
                ChangeState(PlayerStateType.Attack);
        }

        public void CounterAttackStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Attack)) return;

            if (InputManager.instance.GetActionDown("CounterAttack"))
            {
                PlayerParryState parryState = (PlayerParryState)StateCollection[PlayerStateType.Parry];
                if (parryState.wasPerfectParried)
                {
                    attackState.isCounterAttack = true;
                    ChangeState(PlayerStateType.Attack);
                }
            }
        }

        public void ParryStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Parry)) return;
            if (player.canParry && InputManager.instance.GetActionDown("Parry"))
                ChangeState(PlayerStateType.Parry, true);
        }

        public void HealStateChecks()
        {
            if (!StateCollection.ContainsKey(PlayerStateType.Heal)) return;
            if (InputManager.instance.GetAction("Heal"))
            {
                ChangeState(PlayerStateType.Heal);
            }
        }

        public void GoToHurtState()
        {
            if (CurrentStateType != PlayerStateType.Death)
                ChangeState(PlayerStateType.Hurt, true);
        }

        public void GoToDeathState()
        {
            ChangeState(PlayerStateType.Death);
        }

        public void ChangeState(PlayerStateType targetState, bool forceSameState = false)
        {
            if (!forceSameState && CurrentStateType == targetState) return;

            CurrentState?.OnStateExit();
            CurrentState = StateCollection[targetState];
            CurrentState?.OnStateEnter();
        }
    }
}
