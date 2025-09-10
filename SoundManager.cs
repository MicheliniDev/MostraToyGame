using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ToyGame
{
    [DefaultExecutionOrder(-40)]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [SerializeField] private AudioSource ambienceSource;
        [SerializeField] private AudioLowPassFilter ambienceLowPass;

        public float SFXVolume;
        public float AmbienceVolume;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;
        }

        public void MuffleBGM(float muffleDuration)
        {
            if (ambienceSource.clip == null) return;
            StartCoroutine(SmoothMuffleBGM(muffleDuration));
        }

        public void UnmuffleBGM(float unmuffleduration)
        {
            if (ambienceSource.clip == null) return;
            StartCoroutine(SmoothUnmuffleBGM(unmuffleduration));
        }

        private IEnumerator SmoothMuffleBGM(float duration)
        {
            float startValue = 5000f;
            float targetValue = 400f;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float lerpValue = elapsedTime / duration;
                ambienceLowPass.cutoffFrequency = Mathf.Lerp(startValue, targetValue, lerpValue);
            }
            ambienceLowPass.cutoffFrequency = targetValue;
            yield return null;
        }

        private IEnumerator SmoothUnmuffleBGM(float duration)
        {
            float startValue = 400f;
            float targetValue = 5000f;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float lerpValue = elapsedTime / duration;
                ambienceLowPass.cutoffFrequency = Mathf.Lerp(startValue, targetValue, lerpValue);
            }
            ambienceLowPass.cutoffFrequency = targetValue;
            yield return null;
        }

        public void NotifyAmbience(AudioClip clip, float delayDuration)
        {
            if (ambienceSource.clip == clip)
            {
                Debug.Log("Clip is already playing");
                return;
            }
            if (clip == null)
            {
                StartCoroutine(FadeOutAmbience(1f));
                return;
            }
            StartCoroutine(FadeInAmbience(clip, delayDuration));
        }

        private IEnumerator FadeOutAmbience(float duration)
        {
            float startVolume = ambienceSource.volume;
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                ambienceSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
                yield return null;
            }
            ambienceSource.volume = 0f; 
            ambienceSource.Stop();
            ambienceSource.clip = null; 
        }

        private IEnumerator FadeInAmbience(AudioClip clip, float duration)
        {
            if (ambienceSource.clip != clip)  
            {
                yield return StartCoroutine(FadeOutAmbience(0.5f));
            }

            ambienceSource.clip = clip;
            ambienceSource.volume = 0f; 
            ambienceSource.Play();

            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                ambienceSource.volume = Mathf.Lerp(0f, AmbienceVolume, t / duration);
                yield return null;
            }
            ambienceSource.volume = AmbienceVolume;
        }

        public void SetSoundVolume(Slider slider)
        {
            AmbienceVolume = slider.value;
            ambienceSource.volume = AmbienceVolume;
            SFXVolume = slider.value;
        }
    }
}