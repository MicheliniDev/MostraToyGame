using System;
using System.Collections;
using UnityEngine;

namespace ToyGame
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;
        }

        public void PauseTimeForDuration(float duration)
        {
            StartCoroutine(PauseTime(duration));
        }

        private IEnumerator PauseTime(float duration)
        {
            PauseTime();
            yield return new WaitForSecondsRealtime(duration);
            ResumeTime();
        }

        public void PauseTime() => Time.timeScale = 0f;
        public void ResumeTime() => Time.timeScale = 1f;
    }
}
