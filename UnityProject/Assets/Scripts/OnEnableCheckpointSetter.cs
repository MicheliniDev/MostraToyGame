using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class OnEnableCheckpointSetter : MonoBehaviour
    {
        private void OnEnable()
        {
            if (Player.instance == null)
                StartCoroutine(WaitForPlayer());
            else
                Player.instance.SetCheckPoint(transform);
        }

        private IEnumerator WaitForPlayer()
        {
            yield return new WaitUntil(() => Player.instance != null);
            yield return null;
            Player.instance.SetCheckPoint(transform);
        }
    }
}
