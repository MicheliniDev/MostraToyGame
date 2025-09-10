using UnityEngine;

namespace ToyGame.FSM
{
    public class EnemyStateAdder : MonoBehaviour
    {
        private EnemyFSMController fsm;
        private EnemyState bindState;
        
        private void OnEnable()
        {
            fsm = GetComponentInParent<EnemyFSMController>();
            bindState = GetComponent<EnemyState>();

            fsm.AddToStateMachine(bindState.StateType, bindState);
        }
    }
}
