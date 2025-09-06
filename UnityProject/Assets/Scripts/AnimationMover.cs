using UnityEngine;
using ToyGame.Physics;

namespace ToyGame
{
    public class AnimationMover : MonoBehaviour
    {
        private PhysicsMover mover;
        private Animator anim;
        private void Awake()
        {
            mover = GetComponentInParent<PhysicsMover>();    
            anim = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if (anim.deltaPosition.magnitude > 0.001f)
            {
                Vector2 velocity = anim.deltaPosition / Time.deltaTime;
                mover.ApplyAnimationMovement(velocity);
            }
        }
    }
}
