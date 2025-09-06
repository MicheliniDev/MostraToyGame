using System;
using System.Collections.Generic;
using ToyGame.FSM;
using UnityEngine;
using UnityEngine.Events;

namespace ToyGame
{
    public class EnemyHealth : Health
    {
        [SerializeField] private List<EnemyStateType> EnemyDieStates = new();
        private Enemy enemy => GetComponentInParent<Enemy>();

        public UnityEvent OnEnemyDamaged;
        public UnityEvent OnEnemyDeath;

        public Quaternion attackRotation;
        public override void LoseHealthByDealer(DamageDealer dealer)
        {
            base.LoseHealthByDealer(dealer);
            if (dealer is EnemyDamageDealer)
            {
                var checkProjectile = dealer as EnemyDamageDealer;
                if (checkProjectile.parryData is ParryDataProjectile)
                {
                    var projectileData = checkProjectile.parryData as ParryDataProjectile;
                    Destroy(projectileData.transform.parent.gameObject);
                }
            }
            attackRotation = dealer.transform.rotation;
            OnEnemyDamaged?.Invoke();
        }

        public override void HandleDeath()
        {
            base.HandleDeath();
            FetchDeathState(out var deathState);
            enemy.fsm.ChangeState(deathState);
            OnEnemyDeath?.Invoke();
        }

        public void FetchDeathState(out EnemyStateType deathState)
        {
            deathState = EnemyDieStates[0];
            EnemyDieStates.RemoveAt(0);
        }
    }
}