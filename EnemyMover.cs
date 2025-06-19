using UnityEngine;

namespace ToyGame.Physics
{
    public class EnemyMover : PhysicsMover
    {
        void FixedUpdate()
        {
            ApplyFinalVelocity();
        }
    }
}
