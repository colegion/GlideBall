//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Assets/InputMap.inputactions
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

public partial class @InputController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""General Inputs"",
            ""id"": ""4aadd4c1-a435-4ff5-85b6-fe2f51d7f329"",
            ""actions"": [
                {
                    ""name"": ""Tilt"",
                    ""type"": ""Value"",
                    ""id"": ""847fafd8-a0ef-4079-98ee-2b5415da08f4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""OnTap"",
                    ""type"": ""Button"",
                    ""id"": ""271e7e0b-dbc7-44b6-9d69-8ea478c5c4a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TriggerGlide"",
                    ""type"": ""Button"",
                    ""id"": ""a34faa51-22bb-4190-b0d6-ea9ead6308a3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Glide"",
                    ""type"": ""Value"",
                    ""id"": ""4bca35bf-e3eb-4b64-a13c-3711fba5ef0d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6dde186a-8244-40c4-bd0a-b4af2c91dc08"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""3dd4e411-7124-4088-a543-4e0fe7c62c56"",
                    ""path"": ""<Mouse>/position/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""e98dee9b-ca5d-465e-a0e2-c0c5800e8753"",
                    ""path"": ""<Mouse>/position/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cf9b835d-621d-4889-bced-9a60fa2bf12e"",
                    ""path"": ""<Touchscreen>/touch*/Press"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d23e674b-96b9-4683-b105-33993661a2bb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39fc555d-80f7-45dd-8212-b0c6a33ea02d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerGlide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""185dcd6b-0a05-442d-b83c-61188170dfd8"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Glide"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0320c265-0c15-463e-b221-e474eae1acb3"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Glide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64928b7c-8422-4830-9574-d3b11708e81f"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Glide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // General Inputs
        m_GeneralInputs = asset.FindActionMap("General Inputs", throwIfNotFound: true);
        m_GeneralInputs_Tilt = m_GeneralInputs.FindAction("Tilt", throwIfNotFound: true);
        m_GeneralInputs_OnTap = m_GeneralInputs.FindAction("OnTap", throwIfNotFound: true);
        m_GeneralInputs_TriggerGlide = m_GeneralInputs.FindAction("TriggerGlide", throwIfNotFound: true);
        m_GeneralInputs_Glide = m_GeneralInputs.FindAction("Glide", throwIfNotFound: true);
    }

    ~@InputController()
    {
        UnityEngine.Debug.Assert(!m_GeneralInputs.enabled, "This will cause a leak and performance issues, InputController.GeneralInputs.Disable() has not been called.");
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

    // General Inputs
    private readonly InputActionMap m_GeneralInputs;
    private List<IGeneralInputsActions> m_GeneralInputsActionsCallbackInterfaces = new List<IGeneralInputsActions>();
    private readonly InputAction m_GeneralInputs_Tilt;
    private readonly InputAction m_GeneralInputs_OnTap;
    private readonly InputAction m_GeneralInputs_TriggerGlide;
    private readonly InputAction m_GeneralInputs_Glide;
    public struct GeneralInputsActions
    {
        private @InputController m_Wrapper;
        public GeneralInputsActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tilt => m_Wrapper.m_GeneralInputs_Tilt;
        public InputAction @OnTap => m_Wrapper.m_GeneralInputs_OnTap;
        public InputAction @TriggerGlide => m_Wrapper.m_GeneralInputs_TriggerGlide;
        public InputAction @Glide => m_Wrapper.m_GeneralInputs_Glide;
        public InputActionMap Get() { return m_Wrapper.m_GeneralInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralInputsActions set) { return set.Get(); }
        public void AddCallbacks(IGeneralInputsActions instance)
        {
            if (instance == null || m_Wrapper.m_GeneralInputsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GeneralInputsActionsCallbackInterfaces.Add(instance);
            @Tilt.started += instance.OnTilt;
            @Tilt.performed += instance.OnTilt;
            @Tilt.canceled += instance.OnTilt;
            @OnTap.started += instance.OnOnTap;
            @OnTap.performed += instance.OnOnTap;
            @OnTap.canceled += instance.OnOnTap;
            @TriggerGlide.started += instance.OnTriggerGlide;
            @TriggerGlide.performed += instance.OnTriggerGlide;
            @TriggerGlide.canceled += instance.OnTriggerGlide;
            @Glide.started += instance.OnGlide;
            @Glide.performed += instance.OnGlide;
            @Glide.canceled += instance.OnGlide;
        }

        private void UnregisterCallbacks(IGeneralInputsActions instance)
        {
            @Tilt.started -= instance.OnTilt;
            @Tilt.performed -= instance.OnTilt;
            @Tilt.canceled -= instance.OnTilt;
            @OnTap.started -= instance.OnOnTap;
            @OnTap.performed -= instance.OnOnTap;
            @OnTap.canceled -= instance.OnOnTap;
            @TriggerGlide.started -= instance.OnTriggerGlide;
            @TriggerGlide.performed -= instance.OnTriggerGlide;
            @TriggerGlide.canceled -= instance.OnTriggerGlide;
            @Glide.started -= instance.OnGlide;
            @Glide.performed -= instance.OnGlide;
            @Glide.canceled -= instance.OnGlide;
        }

        public void RemoveCallbacks(IGeneralInputsActions instance)
        {
            if (m_Wrapper.m_GeneralInputsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGeneralInputsActions instance)
        {
            foreach (var item in m_Wrapper.m_GeneralInputsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GeneralInputsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GeneralInputsActions @GeneralInputs => new GeneralInputsActions(this);
    public interface IGeneralInputsActions
    {
        void OnTilt(InputAction.CallbackContext context);
        void OnOnTap(InputAction.CallbackContext context);
        void OnTriggerGlide(InputAction.CallbackContext context);
        void OnGlide(InputAction.CallbackContext context);
    }
}
