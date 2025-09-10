using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ToyGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private CanvasGroup loadingScreenGroup;
        [SerializeField] private CanvasGroup pauseGroup;

        public GameObject PlayerWrapper;
        public GameObject UICanvas;
        public GameObject SettingsPanel;
        public Player player;

        public bool isPaused;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame()
        {
            UICanvas.SetActive(true);
            PlayerWrapper.SetActive(true);
            player.Checkpoint = new PlayerCheckpoint() {
                scene = SceneManager.GetActiveScene(),
                position = new Vector3(-5.86f, 1, 0.01120768f),
            };
            InputManager.instance.SwitchCurrentActionMap(InputMap.Gameplay);
            player.transform.position = new Vector3(-5.86f, 1f, 0.01120768f);
        }

        public void QuitGame()
        {
            StartCoroutine(Quit());
        }

        private IEnumerator Quit()
        {
            Time.timeScale = 1f;
            yield return StartCoroutine(FadeInLoadingScreen());

            PlayerWrapper.SetActive(false);

            AsyncOperation quitOperation = SceneManager.LoadSceneAsync("Menu");
            while (!quitOperation.isDone)
            {
                if (quitOperation.progress > 0.95f)
                {
                    quitOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            Resume();
            yield return StartCoroutine(FadeOutLoadingScreen());

            InputManager.instance.SwitchCurrentActionMap(InputMap.UI);
        }

        public void Pause()
        {
            TimeManager.instance.PauseTime();
            isPaused = true;
            FadeInPauseMenu();
            SoundManager.instance.MuffleBGM(.5f);
            InputManager.instance.SwitchCurrentActionMap(InputMap.UI);
        }

        public void Resume()
        {
            TimeManager.instance.ResumeTime();
            isPaused = false;
            FadeOutPauseMenu();
            SoundManager.instance.UnmuffleBGM(.5f);
            SettingsPanel.SetActive(false);
            StartCoroutine(WaitForFrame());
        }

        IEnumerator WaitForFrame()
        {
            yield return null;
            InputManager.instance.SwitchCurrentActionMap(InputMap.Gameplay);
        }

        public void LoadLevel(SceneConnection levelConnection)
        {
            StartCoroutine(SmoothLoadLevel(levelConnection));
        }

        private IEnumerator SmoothLoadLevel(SceneConnection connection)
        {
            player.health.BecomeInvincible();
            yield return StartCoroutine(FadeInLoadingScreen());

            AsyncOperation loading = SceneManager.LoadSceneAsync(connection.scene);
            while (!loading.isDone)
            {
                if (loading.progress > 0.9f)
                {
                    loading.allowSceneActivation = true;
                }
                yield return null;
            }

            player.transform.position = connection.playerSpawnPoint;
            yield return StartCoroutine(FadeOutLoadingScreen());

            player.health.RemoveInvincible();
            yield return null;
        }

        public void FadeLoadingScreen() => StartCoroutine(FadeInLoadingScreen());
        public void FadeLoadingScreenOut() => StartCoroutine(FadeOutLoadingScreen());

        private IEnumerator FadeInLoadingScreen()
        {
            float startAlpha = loadingScreenGroup.alpha;
            float elapsedTime = 0f;
            while (elapsedTime < 0.5f)
            {
                float lerpValue = elapsedTime / 0.3f;
                loadingScreenGroup.alpha = Mathf.Lerp(startAlpha, 1f, lerpValue); 
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            loadingScreenGroup.alpha = 1f;
            yield return null;
        }

        private IEnumerator FadeOutLoadingScreen()
        {
            float startAlpha = loadingScreenGroup.alpha;
            float elapsedTime = 0f;
            while (elapsedTime < 0.5f)
            {
                float lerpValue = elapsedTime / 0.3f;
                loadingScreenGroup.alpha = Mathf.Lerp(startAlpha, 0f, lerpValue);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            loadingScreenGroup.alpha = 0f;
            yield return null;
        }

        public void FadeInPauseMenu()
        {
            StartCoroutine(FadeInPause(.3f));
        }

        public void FadeOutPauseMenu()
        {
            StartCoroutine(FadeOutPause(.2f));
        }

        private IEnumerator FadeInPause(float duration)
        {
            pauseGroup.gameObject.SetActive(true);
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float lerpValue = elapsedTime / duration;
                pauseGroup.alpha = Mathf.Lerp(0f, 1f, lerpValue);
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }
            pauseGroup.alpha = 1f;
            yield return null;
        }

        private IEnumerator FadeOutPause(float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float lerpValue = elapsedTime / duration;
                pauseGroup.alpha = Mathf.Lerp(1f, 0f, lerpValue);
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }
            pauseGroup.alpha = 0f;
            pauseGroup.gameObject.SetActive(false);
            yield return null;
        }
    }
}
