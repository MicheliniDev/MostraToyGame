using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        [SerializeField] private EnemyFSMController fsm;

        public void SetTag(AnimationEvents tag)
        {
            fsm.CurrentState.OnAnimationEvent(tag);
        }

        public enum AnimationEvents { 
            Done,
            StopFlipCheck,
            UnLock
        }
    }
}
