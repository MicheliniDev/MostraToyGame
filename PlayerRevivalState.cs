using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame.FSM
{
    public class PlayerRevivalState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Revival;

        public bool isDeathBySkoteinix0;
        public override void OnAnimationEvent(PlayerAnimationEvents.PlayerAnimationEventTag tag)
        {
            base.OnAnimationEvent(tag);
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

            Debug.Log(player.Checkpoint.scene.name);

            if (!isDeathBySkoteinix0)
            {
                if (player.Checkpoint.scene.name == null)
                {
                    player.Checkpoint.scene = SceneManager.GetActiveScene();
                    yield return null;
                }
                AsyncOperation sceneReload = SceneManager.LoadSceneAsync(player.Checkpoint.scene.name);
                while (!sceneReload.isDone)
                {
                    if (sceneReload.progress > 0.95f)
                    {
                        sceneReload.allowSceneActivation = true;
                    }
                    yield return null;
                }
                player.transform.position = player.Checkpoint.position;
            }
            else
            {
                AsyncOperation sceneReload = SceneManager.LoadSceneAsync("MundoSonhos");
                while (!sceneReload.isDone)
                {
                    if (sceneReload.progress > 0.95f)
                    {
                        sceneReload.allowSceneActivation = true;
                    }
                    yield return null;
                }
                player.transform.position = Vector2.zero;
            }
            player.health.RemoveInvincible();
            player.CUrrentHealAmount = player.MaxHealAmount;
            fsm.ChangeState(PlayerStateType.Normal);
            yield return new WaitForSeconds(.2f);

            GameManager.instance.FadeLoadingScreenOut();
            isDeathBySkoteinix0 = false;
            yield return null;
        }
    }
}
