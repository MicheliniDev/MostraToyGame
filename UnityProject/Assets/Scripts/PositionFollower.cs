using UnityEngine;

namespace ToyGame
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField] private Transform targetPos;

        private void OnEnable()
        {
            if (targetPos != null)
            {
                transform.position = targetPos.position;
                transform.localScale = targetPos.localScale;
            }
        }
        private void Update()
        {
            if (targetPos != null)
            {
                transform.position = targetPos.position;
                transform.localScale = targetPos.localScale;
            }
        }
    }
}
