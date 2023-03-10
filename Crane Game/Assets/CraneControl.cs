//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/CraneControl.inputactions
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

public partial class @CraneControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CraneControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CraneControl"",
    ""maps"": [
        {
            ""name"": ""Crane"",
            ""id"": ""a275582f-6b00-49eb-9634-595b97bf369f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c4c032a9-9b4e-41e7-a439-12acb45e5dac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Lower"",
                    ""type"": ""Button"",
                    ""id"": ""9ffb30d4-0edd-4c08-8d4b-0eaeb82e3d9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""936122ae-0fa7-4e2d-8c6d-c3c62384fa49"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9c8b1981-d91d-4112-bb8c-4784caae3205"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b4250075-592d-4822-832d-58376d2711f2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""90072834-2fbb-4fff-90bf-5c1769731ca6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6af3f0a7-2df6-40b2-b62e-dc7203a35589"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d3c2db7-c63d-48fe-97a4-39c9dd370d1a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c79453d0-d53e-4d96-8a61-78516168f782"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Crane
        m_Crane = asset.FindActionMap("Crane", throwIfNotFound: true);
        m_Crane_Move = m_Crane.FindAction("Move", throwIfNotFound: true);
        m_Crane_Lower = m_Crane.FindAction("Lower", throwIfNotFound: true);
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

    // Crane
    private readonly InputActionMap m_Crane;
    private ICraneActions m_CraneActionsCallbackInterface;
    private readonly InputAction m_Crane_Move;
    private readonly InputAction m_Crane_Lower;
    public struct CraneActions
    {
        private @CraneControl m_Wrapper;
        public CraneActions(@CraneControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Crane_Move;
        public InputAction @Lower => m_Wrapper.m_Crane_Lower;
        public InputActionMap Get() { return m_Wrapper.m_Crane; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CraneActions set) { return set.Get(); }
        public void SetCallbacks(ICraneActions instance)
        {
            if (m_Wrapper.m_CraneActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CraneActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CraneActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CraneActionsCallbackInterface.OnMove;
                @Lower.started -= m_Wrapper.m_CraneActionsCallbackInterface.OnLower;
                @Lower.performed -= m_Wrapper.m_CraneActionsCallbackInterface.OnLower;
                @Lower.canceled -= m_Wrapper.m_CraneActionsCallbackInterface.OnLower;
            }
            m_Wrapper.m_CraneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Lower.started += instance.OnLower;
                @Lower.performed += instance.OnLower;
                @Lower.canceled += instance.OnLower;
            }
        }
    }
    public CraneActions @Crane => new CraneActions(this);
    public interface ICraneActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLower(InputAction.CallbackContext context);
    }
}
