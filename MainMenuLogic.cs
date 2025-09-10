using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ToyGame
{
    public class MainMenuLogic : MonoBehaviour
    {
        [SerializeField] private SceneField startScene;
        [SerializeField] private Image fadeOutImage;
        public void StartGame()
        {
            StartCoroutine(LoadAfterFadeOut(1f));
        }

        private IEnumerator LoadAfterFadeOut(float fadeDuration)
        {
            Color startColor = fadeOutImage.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float lerpValue = elapsedTime / fadeDuration;
                fadeOutImage.color = Color.Lerp(startColor, targetColor, lerpValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            fadeOutImage.color = targetColor;
            SceneManager.LoadSceneAsync(startScene.SceneName);
            GameManager.instance.StartGame();
        }


        public void SetAudioVolume(Slider volume)
        {
            SoundManager.instance.AmbienceVolume = volume.value;
            SoundManager.instance.SFXVolume = volume.value;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
