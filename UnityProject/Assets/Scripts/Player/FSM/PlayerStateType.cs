using UnityEngine;

namespace ToyGame.FSM   
{
    public enum PlayerStateType 
    // um enum dos estados q o player pode ter **
    {
        Normal,
        Attack,
        Parry,
        Dash,
        Hurt,
        Death,
        Revival,
        Heal
    }
}
