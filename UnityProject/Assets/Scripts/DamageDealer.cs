using System;
using System.Collections.Generic;
using ToyGame.Physics;
using UnityEngine;

namespace ToyGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDealer : MonoBehaviour
    {
        private List<Health> damagedEntities;
        public float DamageValue;
        private void Reset()
        {
            var collider = GetComponent<BoxCollider2D>().isTrigger = true;
        }

        protected virtual void OnEnable() => damagedEntities = new List<Health>();
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<Health>(out var receiver)) return;
            if (damagedEntities.Contains(receiver)) return;
            
            receiver?.LoseHealthByDealer(this);
            damagedEntities.Add(receiver);
        }
    }
}
