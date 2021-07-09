// GENERATED AUTOMATICALLY FROM 'Assets/_Input/XRController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @XRController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @XRController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRController"",
    ""maps"": [
        {
            ""name"": ""Oculus Touch"",
            ""id"": ""2660024b-bdae-453e-bbe5-c13169dd03f2"",
            ""actions"": [
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""a1508c2a-5800-4a96-9d6b-cb90e787e2c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6783b259-f091-47c2-acbf-48fcd1e6a4fd"",
                    ""path"": ""<OculusTouchController>/start"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Oculus Controller"",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Oculus Controller"",
            ""bindingGroup"": ""Oculus Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<OculusTouchController>{LeftHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<OculusTouchController>{RightHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Oculus Touch
        m_OculusTouch = asset.FindActionMap("Oculus Touch", throwIfNotFound: true);
        m_OculusTouch_OpenMenu = m_OculusTouch.FindAction("OpenMenu", throwIfNotFound: true);
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

    // Oculus Touch
    private readonly InputActionMap m_OculusTouch;
    private IOculusTouchActions m_OculusTouchActionsCallbackInterface;
    private readonly InputAction m_OculusTouch_OpenMenu;
    public struct OculusTouchActions
    {
        private @XRController m_Wrapper;
        public OculusTouchActions(@XRController wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenMenu => m_Wrapper.m_OculusTouch_OpenMenu;
        public InputActionMap Get() { return m_Wrapper.m_OculusTouch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OculusTouchActions set) { return set.Get(); }
        public void SetCallbacks(IOculusTouchActions instance)
        {
            if (m_Wrapper.m_OculusTouchActionsCallbackInterface != null)
            {
                @OpenMenu.started -= m_Wrapper.m_OculusTouchActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.performed -= m_Wrapper.m_OculusTouchActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.canceled -= m_Wrapper.m_OculusTouchActionsCallbackInterface.OnOpenMenu;
            }
            m_Wrapper.m_OculusTouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenMenu.started += instance.OnOpenMenu;
                @OpenMenu.performed += instance.OnOpenMenu;
                @OpenMenu.canceled += instance.OnOpenMenu;
            }
        }
    }
    public OculusTouchActions @OculusTouch => new OculusTouchActions(this);
    private int m_OculusControllerSchemeIndex = -1;
    public InputControlScheme OculusControllerScheme
    {
        get
        {
            if (m_OculusControllerSchemeIndex == -1) m_OculusControllerSchemeIndex = asset.FindControlSchemeIndex("Oculus Controller");
            return asset.controlSchemes[m_OculusControllerSchemeIndex];
        }
    }
    public interface IOculusTouchActions
    {
        void OnOpenMenu(InputAction.CallbackContext context);
    }
}
