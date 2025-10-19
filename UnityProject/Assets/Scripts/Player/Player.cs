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
    public class Player : MonoBehaviour, IFacingFlippable, IAnimationPlayer // herdando do IFacingFlippable e IAnomationPlayer
    {
        public static Player instance { get; private set; }
        // atributo de instancia do player
        Facings IFacingFlippable.CurrentFacing { get; set; } = Facings.Right;
        // atributo da orientação atual do player, vindo do IFacingFlippable
        Transform IFacingFlippable.transform => transform;
        // **
        public bool CanFlip { get; set; }
        // atributo do player para determinar q ele pode virar

        public Animator anim => GetComponentInChildren<Animator>();
        // variavel publica do animator do player, pegando o animatior de um componente filho

        public PlayerMover playerMover;
        // instancia de PlayerMover
        public PlayerFSMController fsm;
        // instancia de PlayerFSMController
        public PlayerHealth health;
        // instancia da vida de PlayerHealth
        public GameObject PlayerSprite;
        // variavel GameObject do sprite do player

        public bool canParry;
        // variavel boolean para definir o parry

        public PlayerCheckpoint Checkpoint;
        // **

        public int MaxHealAmount = 4;
        // variavel int para a quantidade de cura q o player pode ter
        public int CUrrentHealAmount;
        // variavel int para saber qual a quantide atual de cura q o player tem
        [SerializeField] private GameObject[] healToysUI;
        // **   | variavel q define o GameObject/sprite do boneco da cura

        public SO_Dialogue test;
        // variavel do ScriptableObject de dialogo
        void Awake()
        {
            if (instance != null && instance != this)
                // oq é instance
            {
                Destroy(gameObject);
            }
            instance = this;

            CUrrentHealAmount = MaxHealAmount;
            // define q a quantidade de cura atual é igual a quantidade de cura maxima
            UpdateHealToys();
            // função q atualiza as cura
        }
        // 

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
                DialogueManager.instance.StartDialogue(test);
            // se apertar a tecla delete inicia o dialogo teste, vem do DialogueManager > instance > StartDialogue

            InteractableChecks();
            PauseChecks();
        }

        private void InteractableChecks()
            // checagem dos interagiveis
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
            // ** ?
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

        public void UpdateHealToys()
        {
            for (int i = 0; i < MaxHealAmount; i++)
            {
                healToysUI[i].SetActive(i < CUrrentHealAmount);
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
            UpdateHealToys();
            SceneManager.LoadSceneAsync(Checkpoint.scene.name);
        }
    }

    [System.Serializable]
    public struct PlayerCheckpoint {
        public Scene scene;
        public Vector2 position;
    }
}
