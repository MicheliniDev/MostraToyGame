using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame.FSM
{
    public class PlayerRevivalState : PlayerState
    {
        public override PlayerStateType StateType => PlayerStateType.Revival;

        public bool isDeathBySkoteinix0;
        public override void OnStateEnter()
        {
            StartCoroutine(Revive());
        }
        
        private IEnumerator Revive()
        {
            GameManager.instance.FadeLoadingScreen();
            yield return new WaitForSeconds(.5f);

            player.health.BecomeInvincible();
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
            player.health.GainFull();
            player.health.RemoveInvincible();
            player.CUrrentHealAmount = player.MaxHealAmount;
            player.UpdateHealToys();
            fsm.ChangeState(PlayerStateType.Normal);
            GameManager.instance.FadeLoadingScreenOut();
            yield return new WaitForSeconds(.3f);

            isDeathBySkoteinix0 = false;
            Player.instance.playerMover.CanJump = true;
            yield return null;
        }
    }
}
