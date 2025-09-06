using UnityEngine;
using ToyGame.FSM;
using ToyGame.Physics;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

namespace ToyGame
{
    public class Player : MonoBehaviour, IFacingFlippable, IAnimationPlayer
    {
        public static Player instance { get; private set; }

        Facings IFacingFlippable.CurrentFacing { get; set; } = Facings.Right;
        Transform IFacingFlippable.transform => transform;
        public bool CanFlip { get; set; }

        public Animator anim => GetComponentInChildren<Animator>();

        public PlayerMover playerMover;
        public PlayerFSMController fsm;
        public PlayerHealth health;
        public GameObject PlayerSprite;

        public bool canParry;

        public PlayerCheckpoint Checkpoint;

        public SO_Dialogue test;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;

            Checkpoint = new PlayerCheckpoint();
            Checkpoint.scene = SceneManager.GetActiveScene();
            Checkpoint.position = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
                DialogueManager.instance.StartDialogue(test);

            InteractableChecks();
            PauseChecks();
        }

        private void InteractableChecks()
        {
            if (InputManager.instance.GetActionDown("Interact"))
            {
                Collider2D interactable = Physics2D.OverlapCircle(transform.position, 2f, 1 << LayerMask.NameToLayer("Interactable"));
                if (!interactable) return;
                
                if (interactable.TryGetComponent<IInteractable>(out var component))
                {
                    component?.Interact();
                }
            }
        }

        private void PauseChecks()
        {
            if (InputManager.instance.GetActionDown("Pause") && !GameManager.instance.isPaused)
            {
                Pause();
            }

            if (InputManager.instance.GetActionDown("Resume") && GameManager.instance.isPaused)
            {
                Resume();
            }
        }

        public void Pause()
        {
            TimeManager.instance.PauseTime();
            GameManager.instance.isPaused = true;
            GameManager.instance.FadeInPauseMenu();
            SoundManager.instance.MuffleBGM(.5f);
            InputManager.instance.SwitchCurrentActionMap(InputMap.UI);
        }

        public void Resume()
        {
            TimeManager.instance.ResumeTime();
            GameManager.instance.isPaused = false;
            GameManager.instance.FadeOutPauseMenu();
            SoundManager.instance.UnmuffleBGM(.5f);
            StartCoroutine(WaitForFrame());
        }

        IEnumerator WaitForFrame()
        {
            yield return null;
            InputManager.instance.SwitchCurrentActionMap(InputMap.Gameplay);
        }
        public Facings GetCurrentFacing()
        {
            Facings facving = (this as IFacingFlippable).CurrentFacing;
            return facving;
        }

        public void SetCheckPoint(Transform position)
        {
            PlayerCheckpoint checkpoint = new PlayerCheckpoint();
            checkpoint.scene = SceneManager.GetActiveScene();
            checkpoint.position = position.position;
            Checkpoint = checkpoint;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
    }

    public struct PlayerCheckpoint {
        public Scene scene;
        public Vector2 position;
    }
}
