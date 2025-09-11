using UnityEngine;
using Unity.Cinemachine;
using ToyGame.Physics;
using System.Collections;

namespace ToyGame
{
    [RequireComponent(typeof(CinemachineFollow), typeof(CinemachineCamera), typeof(CinemachineConfiner2D))]
    public class AutoTargetToPlayerCamera : MonoBehaviour
    {
        private CinemachineFollow cameraFollow;
        private CinemachineCamera cameraCN;
        void Start()
        {
            cameraCN = GetComponent<CinemachineCamera>();
            cameraFollow = GetComponent<CinemachineFollow>();
            cameraCN.Target.TrackingTarget = Player.instance.transform;
            cameraFollow.FollowOffset.x = (int)Player.instance.GetCurrentFacing();
        }

        private void OnEnable()
        {
            PlayerMover.OnPlayerFacingChange += TurnCameraOnPlayerFacing;
        }

        private void OnDisable()
        {
            PlayerMover.OnPlayerFacingChange -= TurnCameraOnPlayerFacing;
        }

        public void TurnCameraOnPlayerFacing(Facings playerFacing)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothFadeCamera(playerFacing));
        }
    
        private IEnumerator SmoothFadeCamera(Facings playerFacing)
        {
            float elapsedTime = 0f;
            float time = 1f;
            float currentX = cameraFollow.FollowOffset.x;
            float targetX = (int)playerFacing * 3;
            while (elapsedTime <= time)
            {
                cameraFollow.FollowOffset.x = Mathf.Lerp(currentX, targetX, elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            cameraFollow.FollowOffset.x = targetX;
            yield return null;
        }
    }
}
