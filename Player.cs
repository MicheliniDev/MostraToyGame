using UnityEngine;
using ToyGame.FSM;
using ToyGame.Physics;
using Unity.VisualScripting;

namespace ToyGame
{
    public class Player : MonoBehaviour, IFacingFlippable, IAnimationPlayer
    {
        Facings IFacingFlippable.CurrentFacing { get; set; } = Facings.Right;
        Transform IFacingFlippable.transform => transform; //ASkibg
        public Animator anim => GetComponentInChildren<Animator>();

        public bool CanFlip { get ; set ; }

        [HideInInspector] public PlayerMover playerMover;
        [HideInInspector] public PlayerFSMController fsm;
        [HideInInspector] public PlayerHealth health;

        public InputReader ínput;
        public SO_PlayerStats stats;
        void Awake()
        {
            playerMover = GetComponent<PlayerMover>();
            fsm = GetComponent<PlayerFSMController>();
            health = GetComponentInChildren<PlayerHealth>();

            Debug.Log(health.IsUnityNull());
        }
    }
}
