using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.VirtualTexturing;

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
        
        GameObject lastSelectedGameObject;
        GameObject currentSelectedGameObject_Recent;
        
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

            RegisterAction("AdvanceDialogue", InputMap.Dialogue, KeyCode.Space, KeyCode.Return, KeyCode.JoystickButton0, KeyCode.JoystickButton1);
            RegisterAction("SkipDialogue", InputMap.Dialogue, KeyCode.Escape, KeyCode.JoystickButton7);
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
                CheckPointerExit();
            }
        }

        public InputMap GetActiveMap() => activeMap;

        private void CheckPointerExit()
        {
            if (EventSystem.current == null) return;
            if (EventSystem.current.currentSelectedGameObject != currentSelectedGameObject_Recent)
            {
                lastSelectedGameObject = currentSelectedGameObject_Recent;
                currentSelectedGameObject_Recent = EventSystem.current.currentSelectedGameObject;
            }
        }

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
