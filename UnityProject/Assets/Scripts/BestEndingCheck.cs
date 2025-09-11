using TMPro;
using UnityEngine;

namespace ToyGame
{
    public class BestEndingCheck : MonoBehaviour
    {
        [SerializeField] private GameObject bigsmallCanvas;
        [SerializeField] private GameObject goodEndingCanvas;
        private bool playerDamaged = false;
        private void OnEnable() => Player.instance.health.OnPlayerHurt.AddListener(SetPlayerDamaged);
        private void OnDisable() => Player.instance.health.OnPlayerHurt.RemoveListener(SetPlayerDamaged);
        public void SetPlayerDamaged() => playerDamaged = true;
        public void OnBossDeathCheckEnding()
        {
            if (playerDamaged)
            {
                goodEndingCanvas.SetActive(true);
            }
            else
            {
                bigsmallCanvas.SetActive(true);
            }
        }
    }
}