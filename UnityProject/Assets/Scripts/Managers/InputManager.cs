using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

namespace ToyGame
{
    [DefaultExecutionOrder(-20)]
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        [SerializeField, Range(0f, 1f)] 
        private float bufferTime = 0.15f;

        private InputMap activeMap = InputMap.Gameplay;

        private Dictionary<string, ActionInput> actions = new();
        private Dictionary<string, AxisInput> axes = new();

        private EventSystem eventSystem;
        private GameObject lastSelected;
        private float nextRepeatTime;

        [Header("UI Navigation Settings")]
        public float repeatDelay = 0.4f;
        public float repeatRate = 0.1f;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            instance = this;

            RegisterAxis("Move", InputMap.Gameplay, "Horizontal");
            RegisterAction("Jump", InputMap.Gameplay, KeyCode.Space, KeyCode.JoystickButton0); //KeyCode.JoystickButton1
            RegisterAction("Attack", InputMap.Gameplay, KeyCode.J, KeyCode.JoystickButton2, KeyCode.Mouse0); //KeyCode.JoystickButton0
            RegisterAction("CounterAttack", InputMap.Gameplay, KeyCode.F, KeyCode.JoystickButton5);
            RegisterAction("Parry", InputMap.Gameplay, KeyCode.K, KeyCode.JoystickButton4, KeyCode.Mouse1);
            RegisterAction("Heal", InputMap.Gameplay, KeyCode.R, KeyCode.JoystickButton3);
            RegisterAction("Interact", InputMap.Gameplay, KeyCode.E, KeyCode.JoystickButton1);
            RegisterAction("Pause", InputMap.Gameplay, KeyCode.Escape, KeyCode.Backspace, KeyCode.JoystickButton7); //KeyCode.JoystickButton9

            RegisterAxis("UINavigateHorizontal", InputMap.UI, "Horizontal");
            RegisterAxis("UINavigateVertical", InputMap.UI, "Vertical");
            RegisterAction("Submit", InputMap.UI, KeyCode.Return, KeyCode.Space, KeyCode.JoystickButton1);
            RegisterAction("Cancel", InputMap.UI, KeyCode.Backspace, KeyCode.JoystickButton0);
            RegisterAction("Resume", InputMap.UI, KeyCode.Escape, KeyCode.JoystickButton7); //KeyCode.JoystickButton9

            RegisterAction("AdvanceDialogue", InputMap.Dialogue, KeyCode.Space, KeyCode.Return, KeyCode.JoystickButton1, KeyCode.JoystickButton2);
        }

        private void Update()
        {
            /*foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                    Debug.Log("KeyCode down: " + kcode);
            }*/

            foreach (var action in actions)
            {
                if (activeMap == action.Value.map)
                    action.Value.Update();
            }

            foreach (var axis in axes)
            {
                if (activeMap == axis.Value.map)
                    axis.Value.Update();
            }

            if (activeMap == InputMap.UI)
            {
                UpdateUI();
            }
        }

        #region UI Handling
        private void UpdateUI()
        {
            if (eventSystem == null) return;

            if (eventSystem.currentSelectedGameObject == null && lastSelected != null)
                eventSystem.SetSelectedGameObject(lastSelected);
            else
                lastSelected = eventSystem.currentSelectedGameObject;

            HandleNavigation();
            HandleSubmitCancel();
        }

        void HandleNavigation()
        {
            Vector2 move = new Vector2(GetAxis("UINavigateHorizontal"), GetAxis("UINavigateVertical"));

            if (move.sqrMagnitude > 0.1f)
            {
                if (Time.time >= nextRepeatTime)
                {
                    SendMove(move);
                    nextRepeatTime = Time.time + repeatDelay;
                }
            }
        }

        private void HandleSubmitCancel()
        {
            if (GetActionDown("Submit"))
            {
                var data = new BaseEventData(eventSystem);
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.submitHandler);
            }

            if (GetActionDown("Cancel"))
            {
                var data = new BaseEventData(eventSystem);
                ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.cancelHandler);
            }
        }

        private void SendMove(Vector2 move)
        {
            var data = new AxisEventData(eventSystem)
            {
                moveVector = move,
                moveDir = GetMoveDirection(move)
            };

            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.moveHandler);
        }

        private MoveDirection GetMoveDirection(Vector2 move)
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                return move.x > 0 ? MoveDirection.Right : MoveDirection.Left;
            else if (move.y != 0)
                return move.y > 0 ? MoveDirection.Up : MoveDirection.Down;
            return MoveDirection.None;
        }
        #endregion

        public float GetAxis(string axisName)
        {
            return axes[axisName].Value;
        }

        public bool GetActionDown(string actionName)
        {
            if (actions.TryGetValue(actionName, out var value))
            {
                return value.ConsumeBuffer();
            }
            return false;
        }

        public bool GetAction(string actionName)
        {
            if (actions.TryGetValue(actionName, out var value))
            {
                return value.IsHeld();
            }
            return false;
        }

        public bool GetActionUp(string actionName)
        {
            if (actions.TryGetValue(actionName, out var value))
            {
                return value.WasReleased();
            }
            return false;
        }

        public void RegisterAction(string actionName, InputMap map, params KeyCode[] keys)
        {
            if (!actions.ContainsKey(actionName))
            {
                actions[actionName] = new ActionInput(map, keys, bufferTime);
            }
        }

        public void RegisterAxis(string axisName, InputMap map, string unityAxis)
        {
            if (!axes.ContainsKey(axisName))
            {
                axes[axisName] = new AxisInput(unityAxis, map);
            }
        }

        public void SwitchCurrentActionMap(InputMap map)
        {
            activeMap = map;
        }
    }

    public class AxisInput
    {
        public InputMap map;

        public float Value;
        public string Axis;
        public AxisInput(string Axis, InputMap map)
        {
            this.map = map;
            this.Axis = Axis;
        }

        public void Update()
        {
            Value = Input.GetAxis(Axis);
        }
    }

    public class ActionInput
    {
        public InputMap map;
        
        private KeyCode[] keys;
        private float bufferTime;
        private float lastPressedTime = -999f;
        private bool wasPressed;
        private bool wasReleased;

        public bool WasPressed { 
            get { 
                return wasPressed; 
            } 
        }
        public ActionInput(InputMap map, KeyCode[] keys, float bufferTime)
        {
            this.map = map;
            this.keys = keys;
            this.bufferTime = bufferTime;
        }

        public void Update()
        {
            wasPressed = false;
            wasReleased = false;

            foreach (var key in keys) 
            {
                if (Input.GetKeyDown(key))
                {
                    wasPressed = true;
                    lastPressedTime = Time.time;
                    break;
                }
            }

            foreach (var key in keys)
            {
                if (Input.GetKeyUp(key))
                {
                    wasReleased = true;
                    break;
                }
            }
        }

        public bool ConsumeBuffer()
        {
            if (Time.time < lastPressedTime + bufferTime)
            {
                lastPressedTime = -999f;
                return true;
            }
            return false;
        }

        public bool IsHeld()
        {
            foreach (var key in keys)
            {
                if (Input.GetKey(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool WasReleased() => wasReleased;
    }

    public enum InputMap
    {
        Gameplay,
        UI,
        Dialogue
    }
}
