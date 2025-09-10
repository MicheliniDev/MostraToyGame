using Unity.Cinemachine;
using UnityEngine;

namespace ToyGame
{
    public class AddGroupTargetToPlayerCamera : MonoBehaviour
    {
        private void Start()
        {
            CinemachineTargetGroup.Target playerTarget = new CinemachineTargetGroup.Target(){};
            playerTarget.Object = Player.instance.transform;
            GetComponent<CinemachineTargetGroup>().Targets.Add(playerTarget);
        }
    }
}
