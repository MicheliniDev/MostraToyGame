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

        public int MaxHealAmount = 4;
        public int CUrrentHealAmount;

        public SO_Dialogue test;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;

            CUrrentHealAmount = MaxHealAmount;
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
            GameManager.instance.Pause();
        }

        public void Resume()
        {
            GameManager.instance.Resume();
        }

        public void Quit()
        {
            GameManager.instance.QuitGame();
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

        public void ResetLevel()
        {
            health.GainFull();
            CUrrentHealAmount = MaxHealAmount;
            SceneManager.LoadSceneAsync(Checkpoint.scene.name);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(250f, 10f, 200f, 100f));
            GUILayout.Label($"<color='green'><size=100>{CUrrentHealAmount}</size></color>");
            GUILayout.EndArea();
        }
    }

    [System.Serializable]
    public struct PlayerCheckpoint {
        public Scene scene;
        public Vector2 position;
    }
}
