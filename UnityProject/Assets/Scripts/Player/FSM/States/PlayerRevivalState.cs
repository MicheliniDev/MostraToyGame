using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame.FSM
{
    public class PlayerRevivalState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Revival;

        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
            if (tag == PlayerAnimationEvents.PlayerAnimationEventTag.Done)
            {
                fsm.ChangeState(PlayerStateType.Normal);
            }
        }

        public override void OnStateEnter()
        {
            StartCoroutine(Revive());
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

        private IEnumerator Revive()
        {
            GameManager.instance.FadeLoadingScreen();
            yield return new WaitForSeconds(.5f);

            player.health.GainFull();
            player.health.BecomeInvincible();
            AsyncOperation sceneReload = SceneManager.LoadSceneAsync(player.Checkpoint.scene.name);
            while (!sceneReload.isDone)
            {
                if (sceneReload.progress > 0.95f)
                {
                    sceneReload.allowSceneActivation = true;
                }
                yield return null;
            }
            player.health.RemoveInvincible();
            player.transform.position = player.Checkpoint.position;
            fsm.ChangeState(PlayerStateType.Normal);
            yield return new WaitForSeconds(.2f);

            GameManager.instance.FadeLoadingScreenOut();
            yield return null;
        }
    }
}
