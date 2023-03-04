//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/New test_3d/player_controller.inputactions
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

public partial class @Player_controller : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""player_controller"",
    ""maps"": [
        {
            ""name"": ""player movement"",
            ""id"": ""f330cbc9-c06c-4207-be6b-fd509e4087b1"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c02f9f37-5982-4fa7-9688-a3c0e24ef144"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5884280b-f9b2-45b6-96d2-ad6c951ae6a1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""wasd"",
                    ""id"": ""dec02620-b72d-4ad8-9f2c-0e77a02a0e43"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d2f29006-4eb0-402a-9eeb-28edc71ef1ed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c50b6fa2-f204-491a-99ca-28a8a807106f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""89c134e3-11d2-48e0-a09d-cf90db293b88"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c7029b4c-af2d-4dd8-8f06-d5afe50d9cdb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""71679469-6123-4e4b-afa3-4e6a3c7ea8a7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77f6a723-ca1c-4e84-a289-4b4bd3b43ccc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""player actions"",
            ""id"": ""1eda179f-dd1d-41e6-b839-04512ee028a8"",
            ""actions"": [
                {
                    ""name"": ""roll"",
                    ""type"": ""Button"",
                    ""id"": ""0c44deb2-353c-4b16-8105-0b25681eda0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d625eb85-b9a3-4b8a-9234-1e65d0e7bc92"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7f8a0ea-6651-4ceb-9459-8f57baa60895"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // player movement
        m_playermovement = asset.FindActionMap("player movement", throwIfNotFound: true);
        m_playermovement_movement = m_playermovement.FindAction("movement", throwIfNotFound: true);
        m_playermovement_camera = m_playermovement.FindAction("camera", throwIfNotFound: true);
        // player actions
        m_playeractions = asset.FindActionMap("player actions", throwIfNotFound: true);
        m_playeractions_roll = m_playeractions.FindAction("roll", throwIfNotFound: true);
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

    // player movement
    private readonly InputActionMap m_playermovement;
    private IPlayermovementActions m_PlayermovementActionsCallbackInterface;
    private readonly InputAction m_playermovement_movement;
    private readonly InputAction m_playermovement_camera;
    public struct PlayermovementActions
    {
        private @Player_controller m_Wrapper;
        public PlayermovementActions(@Player_controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_playermovement_movement;
        public InputAction @camera => m_Wrapper.m_playermovement_camera;
        public InputActionMap Get() { return m_Wrapper.m_playermovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayermovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayermovementActions instance)
        {
            if (m_Wrapper.m_PlayermovementActionsCallbackInterface != null)
            {
                @movement.started -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnMovement;
                @movement.performed -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnMovement;
                @movement.canceled -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnMovement;
                @camera.started -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnCamera;
                @camera.performed -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnCamera;
                @camera.canceled -= m_Wrapper.m_PlayermovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayermovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @movement.started += instance.OnMovement;
                @movement.performed += instance.OnMovement;
                @movement.canceled += instance.OnMovement;
                @camera.started += instance.OnCamera;
                @camera.performed += instance.OnCamera;
                @camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayermovementActions @playermovement => new PlayermovementActions(this);

    // player actions
    private readonly InputActionMap m_playeractions;
    private IPlayeractionsActions m_PlayeractionsActionsCallbackInterface;
    private readonly InputAction m_playeractions_roll;
    public struct PlayeractionsActions
    {
        private @Player_controller m_Wrapper;
        public PlayeractionsActions(@Player_controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @roll => m_Wrapper.m_playeractions_roll;
        public InputActionMap Get() { return m_Wrapper.m_playeractions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayeractionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayeractionsActions instance)
        {
            if (m_Wrapper.m_PlayeractionsActionsCallbackInterface != null)
            {
                @roll.started -= m_Wrapper.m_PlayeractionsActionsCallbackInterface.OnRoll;
                @roll.performed -= m_Wrapper.m_PlayeractionsActionsCallbackInterface.OnRoll;
                @roll.canceled -= m_Wrapper.m_PlayeractionsActionsCallbackInterface.OnRoll;
            }
            m_Wrapper.m_PlayeractionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @roll.started += instance.OnRoll;
                @roll.performed += instance.OnRoll;
                @roll.canceled += instance.OnRoll;
            }
        }
    }
    public PlayeractionsActions @playeractions => new PlayeractionsActions(this);
    public interface IPlayermovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayeractionsActions
    {
        void OnRoll(InputAction.CallbackContext context);
    }
}
