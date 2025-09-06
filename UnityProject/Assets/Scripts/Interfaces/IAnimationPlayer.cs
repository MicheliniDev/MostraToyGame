using UnityEngine;

namespace ToyGame
{
    public interface IAnimationPlayer 
    {
        Animator anim { get; }
        public void PlayAnimation(string animationName, bool forceAnimation = false, float normalizedTime = 0f)
        {
            if (anim == null || !anim.isActiveAndEnabled) return;

            int animationHash = Animator.StringToHash(animationName);
            if (anim.HasState(0, animationHash))
            {
                AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
                if (forceAnimation || !currentState.IsName(animationName))
                {
                    anim.Play(animationHash, 0, normalizedTime);
                }
            }
        }
    }
}
