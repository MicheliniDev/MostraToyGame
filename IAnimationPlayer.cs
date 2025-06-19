using UnityEngine;

namespace ToyGame
{
    public interface IAnimationPlayer 
    {
        Animator anim { get; }
        void PlayAnimation(string animationName, bool forceAnimation = false, float normalizedTime = 0f)
        {
            if (forceAnimation)
            {
                if (anim.HasState(0, Animator.StringToHash(animationName)))
                {
                    anim.Play(animationName, 0, normalizedTime);
                }
                else
                {
                    Debug.LogError("No Animation??" + animationName);
                }
            }
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                if (anim.HasState(0, Animator.StringToHash(animationName)))
                {
                    anim.Play(animationName, 0, normalizedTime);
                    return;
                }
                Debug.LogError("No Animation??" + animationName);
            }
        }
    }
}
