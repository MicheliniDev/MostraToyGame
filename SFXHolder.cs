using UnityEngine;

namespace ToyGame
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXHolder : MonoBehaviour
    {
        [SerializeField] private AudioClip[] sfxs;
        private AudioSource source;
        public float minPitch = 1f;
        public float maxPitch = 1f;
        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        private void Reset()
        {
            GetComponent<AudioSource>().playOnAwake = false;
        }

        private void OnEnable()
        {
            var randomSound = sfxs[Random.Range(0, sfxs.Length)];
            source.volume = SoundManager.instance.SFXVolume;
            source.clip = randomSound;
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
        }
    }
}
