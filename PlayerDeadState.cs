using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame.FSM
{
    public class PlayerDeadState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Death;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }
    }
}
