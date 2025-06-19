using System;
using System.Collections.Generic;
using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class EnemyHealth : Health
    {
        [SerializeField] private List<EnemyStateType> EnemyDieStates = new();
        private Enemy enemy => GetComponentInParent<Enemy>();
        public override void HandleDeath()
        {
            base.HandleDeath();
            FetchDeathState(out var deathState);
            enemy.fsm.ChangeState(deathState);
        }

        public void FetchDeathState(out EnemyStateType deathState)
        {
            deathState = EnemyDieStates[0];
            EnemyDieStates.RemoveAt(0);
        }
    }
}