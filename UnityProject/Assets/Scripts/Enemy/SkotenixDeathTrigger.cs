using ToyGame.FSM;
using UnityEngine;

namespace ToyGame
{
    public class SkotenixDeathTrigger : MonoBehaviour
    {
        private void OnEnable()
        {
            ((PlayerRevivalState)Player.instance.fsm.StateCollection[PlayerStateType.Revival]).isDeathBySkoteinix0 = true;
        }
    }
}
