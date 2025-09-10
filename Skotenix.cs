using ToyGame.FSM;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    public class Skotenix : MonoBehaviour
    {
        private void OnEnable()
        {
            ((PlayerRevivalState)Player.instance.fsm.StateCollection[FSM.PlayerStateType.Revival]).isDeathBySkoteinix0 = true;
        }
    }
}
