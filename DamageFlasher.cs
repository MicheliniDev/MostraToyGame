using System.Collections;
using UnityEngine;

namespace ToyGame
{
    public class DamageFlasher : MonoBehaviour
    {
        [SerializeField] private float flashTime = 0.3f;
        [SerializeField] private SpriteRenderer[] spritesToFlash;
        [SerializeField] private AnimationCurve flashCurve;
        
        private Material[] materialsToFlash;
        private Coroutine flashCoroutine;
        public void Awake()
        {
            materialsToFlash = new Material[spritesToFlash.Length];

            for (int i = 0; i < spritesToFlash.Length; i++)
            {
                materialsToFlash[i] = spritesToFlash[i].material;
            }
        }

        public void Flash()
        {
            flashCoroutine = StartCoroutine(FlashSprite());
        }

        private IEnumerator FlashSprite()
        {
            SetFlashColor();

            float currentFlashAmount = 0f;
            float elapsedTime = 0f;
            while (elapsedTime < flashTime)
            {
                elapsedTime += Time.deltaTime;
                currentFlashAmount = Mathf.Lerp(1f, flashCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
                SetFlashAmount(currentFlashAmount);
                yield return null;
            }
        }

        private void SetFlashColor()
        {
            for (int i = 0; i < spritesToFlash.Length; i++)
            {
                materialsToFlash[i].SetColor("_FlashColor", Color.white);
            }
        }

        private void SetFlashAmount(float amount)
        {
            for (int i = 0; i < spritesToFlash.Length; i++)
            {
                materialsToFlash[i].SetFloat("_FlashAmount", amount);
            }
        }
    }
}
