using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class CreditsAnimationLogic : MonoBehaviour
    {
        private Animator anim;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            StartCoroutine(WaitForAnimationEnd());
        }

        private IEnumerator WaitForAnimationEnd()
        {
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                yield return null;
            }
            SceneManager.LoadSceneAsync("Menu");
            yield return null;
        }
    }
}
