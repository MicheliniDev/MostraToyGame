using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ToyGame
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions Input;

        public event Action<Vector2> moveEvent;
        public event Action attackEvent;
        public event Action counterAttackEvent;
        public event Action parryEvent;
        public event Action interactEvent;
        public event Action jumpEvent;
        public event Action jumpCanceledEvent;

        public event Action pauseEvent;

        private void OnEnable()
        {
            if (Input == null)
            {
                Input = new InputSystem_Actions();
                Input.Player.SetCallbacks(this);
                Input.Player.Enable();
            }
        }

        private void OnDisable()
        {
            Input.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                jumpEvent?.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                jumpCanceledEvent?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            attackEvent?.Invoke();
        }

        public void OnCounterAttack(InputAction.CallbackContext context)
        {
            counterAttackEvent?.Invoke();
        }
        public void OnParry(InputAction.CallbackContext context)
        {
            parryEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            interactEvent?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            pauseEvent?.Invoke();
        }

        public void EnableInput()
        {
            Input.Player.Enable();
        }

        public void DisableInput()
        {
            Input.Player.Disable();
        }

        public void SwitchToUI()
        {
            Input.Player.Disable();
            Input.UI.Enable();
        }

        public void SwitchToPlayer()
        {
            Input.UI.Disable();
            Input.Player.Enable();
        }
    }
}
