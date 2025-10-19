using UnityEngine;

namespace ToyGame
{
    public class AmbienceHolder : MonoBehaviour
    {
        [SerializeField] private AudioClip ambience;
        [SerializeField] private float playDelay;
        private void OnEnable()
        {
            SoundManager.instance.NotifyAmbience(ambience, playDelay);
        }
    }
}
