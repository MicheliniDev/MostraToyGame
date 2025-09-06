using UnityEngine;
using ToyGame.FSM;

namespace ToyGame
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private PlayerFSMController fsm;

        public void SetTag(PlayerAnimationEventTag tag)
        {
            fsm.CurrentState?.OnAnimationEvent(tag);
        }

        public enum PlayerAnimationEventTag
        {
            Done,
            Cancel,
            Commited,
            CanMove,
            CanFlip,
            CanParry
        }
    }
}
