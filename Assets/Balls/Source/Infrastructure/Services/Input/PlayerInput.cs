//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Balls/Configs/Player.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""GameBoard"",
            ""id"": ""12d32aab-4ca5-4a69-b6eb-cf6309cc1020"",
            ""actions"": [
                {
                    ""name"": ""Cursor"",
                    ""type"": ""Value"",
                    ""id"": ""8359cf14-e512-4cfb-a082-31709c521f2d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CursorPress"",
                    ""type"": ""Button"",
                    ""id"": ""831950c4-0e9a-4a83-b939-c5a8018ff22f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d809a5a7-3eea-4bcf-80ae-571f7ae39537"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Mouse;Touchscreen"",
                    ""action"": ""Cursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f979350-7fe0-49d8-8f50-817be9883043"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": "";Mouse;Touchscreen"",
                    ""action"": ""CursorPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameBoard
        m_GameBoard = asset.FindActionMap("GameBoard", throwIfNotFound: true);
        m_GameBoard_Cursor = m_GameBoard.FindAction("Cursor", throwIfNotFound: true);
        m_GameBoard_CursorPress = m_GameBoard.FindAction("CursorPress", throwIfNotFound: true);
    }

    ~@PlayerInput()
    {
        UnityEngine.Debug.Assert(!m_GameBoard.enabled, "This will cause a leak and performance issues, PlayerInput.GameBoard.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GameBoard
    private readonly InputActionMap m_GameBoard;
    private List<IGameBoardActions> m_GameBoardActionsCallbackInterfaces = new List<IGameBoardActions>();
    private readonly InputAction m_GameBoard_Cursor;
    private readonly InputAction m_GameBoard_CursorPress;
    public struct GameBoardActions
    {
        private @PlayerInput m_Wrapper;
        public GameBoardActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cursor => m_Wrapper.m_GameBoard_Cursor;
        public InputAction @CursorPress => m_Wrapper.m_GameBoard_CursorPress;
        public InputActionMap Get() { return m_Wrapper.m_GameBoard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameBoardActions set) { return set.Get(); }
        public void AddCallbacks(IGameBoardActions instance)
        {
            if (instance == null || m_Wrapper.m_GameBoardActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameBoardActionsCallbackInterfaces.Add(instance);
            @Cursor.started += instance.OnCursor;
            @Cursor.performed += instance.OnCursor;
            @Cursor.canceled += instance.OnCursor;
            @CursorPress.started += instance.OnCursorPress;
            @CursorPress.performed += instance.OnCursorPress;
            @CursorPress.canceled += instance.OnCursorPress;
        }

        private void UnregisterCallbacks(IGameBoardActions instance)
        {
            @Cursor.started -= instance.OnCursor;
            @Cursor.performed -= instance.OnCursor;
            @Cursor.canceled -= instance.OnCursor;
            @CursorPress.started -= instance.OnCursorPress;
            @CursorPress.performed -= instance.OnCursorPress;
            @CursorPress.canceled -= instance.OnCursorPress;
        }

        public void RemoveCallbacks(IGameBoardActions instance)
        {
            if (m_Wrapper.m_GameBoardActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameBoardActions instance)
        {
            foreach (var item in m_Wrapper.m_GameBoardActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameBoardActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameBoardActions @GameBoard => new GameBoardActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    private int m_TouchscreenSchemeIndex = -1;
    public InputControlScheme TouchscreenScheme
    {
        get
        {
            if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
            return asset.controlSchemes[m_TouchscreenSchemeIndex];
        }
    }
    public interface IGameBoardActions
    {
        void OnCursor(InputAction.CallbackContext context);
        void OnCursorPress(InputAction.CallbackContext context);
    }
}
