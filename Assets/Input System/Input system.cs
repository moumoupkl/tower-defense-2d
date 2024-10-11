//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input System/Input system.inputactions
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

public partial class @Inputsystem: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputsystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input system"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""10501996-4214-4675-b100-b2dfbd8048bc"",
            ""actions"": [
                {
                    ""name"": ""move p1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e45f8657-8f01-4e35-a926-06f96c02db7a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy tower 1 p1"",
                    ""type"": ""Button"",
                    ""id"": ""d261c055-b3d9-4da3-b216-e696931578f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy tower 2  p1"",
                    ""type"": ""Button"",
                    ""id"": ""e657b1e3-2640-41f8-9d2c-f786fe6c1171"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy troupe 1 p1"",
                    ""type"": ""Button"",
                    ""id"": ""b18f3781-ecd6-4004-b7f2-9fcf8a3fa145"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy troupe 2 p1"",
                    ""type"": ""Button"",
                    ""id"": ""5874a2f0-155f-4170-9489-fb6f872bcd7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""open troups p1"",
                    ""type"": ""Button"",
                    ""id"": ""8754857c-893a-4e4a-a268-9a76a904e670"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""move p2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""78b2fc71-fb24-4664-bbe9-97a6f5e2db7f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy tower 1 p2"",
                    ""type"": ""Button"",
                    ""id"": ""68f68a2b-daa3-4e05-bcf6-8b31f3ccc172"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy tower 2 p2"",
                    ""type"": ""Button"",
                    ""id"": ""ddb17fd9-e054-4ffe-ad4a-d4ed2a5de8ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy troupe 1 p2"",
                    ""type"": ""Button"",
                    ""id"": ""1f2e61d2-a805-4200-bdc7-35f55ae41dcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""buy troupe 2 p2"",
                    ""type"": ""Button"",
                    ""id"": ""db8a229d-4555-42df-9e1c-5a41a7c2e461"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0ac0dc30-06e3-4a0d-b69e-0c8e64169f77"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy tower 1 p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20d1afd8-1ffd-4c38-9098-dcb57246a92c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""buy tower 1 p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11443fb3-19ee-4bb9-b572-3915bddfa708"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy tower 2 p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard2"",
                    ""id"": ""19525d59-2958-46e2-901f-eeb630475e97"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move p2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""493c2b96-5a15-4fdf-9801-dcba8ab35fe9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""48a6cd3f-c467-45ed-b921-cd3d54db1363"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""94d88aad-4327-43ff-a85c-96b67c5ecc1c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2183b69d-dba3-4d6e-a752-5892042ad45f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""joystick 2"",
                    ""id"": ""3ebe7c0f-8587-4415-af4b-74d3d1304e8f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move p2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e8338531-795f-46e0-b2e6-971454f269e3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""03c4a849-acd7-4c31-b01a-659361301f85"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1fea0d7c-6133-4a8f-83f2-655536333fa4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3e34d28a-f48d-460e-8ee0-eeefb75c488e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e0789f6d-e7ac-451b-8162-e91d2b1111e6"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buy tower 2  p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2344a5e0-1227-435c-93d4-0e6e0edf0209"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy troupe 1 p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5449221a-5369-4aa1-8c19-a1f25e2d94e5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy troupe 2 p2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a592ec7f-976b-4a5f-b1c6-882e563d0368"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy troupe 2 p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69624ce8-aca9-48d3-b460-75a03ed875ff"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy troupe 1 p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""keyboard1"",
                    ""id"": ""43aed0b2-1e25-46e4-ae7a-4f966d4672e2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move p1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0aa93a98-c013-4f4e-9e31-7813e924f45c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7e25cf14-f665-4078-b5bb-2f1e1ed8b0bc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""904de036-0bbb-4b4e-aec5-93f3b35afb05"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0fdafaf8-b10a-4d0f-bdde-e95690309178"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""joystick1"",
                    ""id"": ""7a16efbc-a677-4d7f-80c6-1edcf20150b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move p1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2d1858ed-9a94-49f8-b5b2-a8ccc6cb4972"",
                    ""path"": ""<DualShockGamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ced448ca-540d-4c31-84d6-7f1ec3c51112"",
                    ""path"": ""<DualShockGamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""95e9063f-a3be-4597-8102-d287f3ce5b54"",
                    ""path"": ""<DualShockGamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bbcc610d-b65a-49f9-a668-8edb52e4f5b9"",
                    ""path"": ""<DualShockGamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""move p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""597cf8b1-955e-48d7-b4c4-e7552e05d33a"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""buy tower 1 p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2fe9b05-3725-4ae9-a1f7-21c2cd61d042"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""controller"",
                    ""action"": ""buy tower 1 p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96ddbf56-06b6-4e03-86c4-7b71ef9a7c75"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""keyboard"",
                    ""action"": ""open troups p1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""keyboard"",
            ""bindingGroup"": ""keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""controller"",
            ""bindingGroup"": ""controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_movep1 = m_Gameplay.FindAction("move p1", throwIfNotFound: true);
        m_Gameplay_buytower1p1 = m_Gameplay.FindAction("buy tower 1 p1", throwIfNotFound: true);
        m_Gameplay_buytower2p1 = m_Gameplay.FindAction("buy tower 2  p1", throwIfNotFound: true);
        m_Gameplay_buytroupe1p1 = m_Gameplay.FindAction("buy troupe 1 p1", throwIfNotFound: true);
        m_Gameplay_buytroupe2p1 = m_Gameplay.FindAction("buy troupe 2 p1", throwIfNotFound: true);
        m_Gameplay_opentroupsp1 = m_Gameplay.FindAction("open troups p1", throwIfNotFound: true);
        m_Gameplay_movep2 = m_Gameplay.FindAction("move p2", throwIfNotFound: true);
        m_Gameplay_buytower1p2 = m_Gameplay.FindAction("buy tower 1 p2", throwIfNotFound: true);
        m_Gameplay_buytower2p2 = m_Gameplay.FindAction("buy tower 2 p2", throwIfNotFound: true);
        m_Gameplay_buytroupe1p2 = m_Gameplay.FindAction("buy troupe 1 p2", throwIfNotFound: true);
        m_Gameplay_buytroupe2p2 = m_Gameplay.FindAction("buy troupe 2 p2", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_movep1;
    private readonly InputAction m_Gameplay_buytower1p1;
    private readonly InputAction m_Gameplay_buytower2p1;
    private readonly InputAction m_Gameplay_buytroupe1p1;
    private readonly InputAction m_Gameplay_buytroupe2p1;
    private readonly InputAction m_Gameplay_opentroupsp1;
    private readonly InputAction m_Gameplay_movep2;
    private readonly InputAction m_Gameplay_buytower1p2;
    private readonly InputAction m_Gameplay_buytower2p2;
    private readonly InputAction m_Gameplay_buytroupe1p2;
    private readonly InputAction m_Gameplay_buytroupe2p2;
    public struct GameplayActions
    {
        private @Inputsystem m_Wrapper;
        public GameplayActions(@Inputsystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @movep1 => m_Wrapper.m_Gameplay_movep1;
        public InputAction @buytower1p1 => m_Wrapper.m_Gameplay_buytower1p1;
        public InputAction @buytower2p1 => m_Wrapper.m_Gameplay_buytower2p1;
        public InputAction @buytroupe1p1 => m_Wrapper.m_Gameplay_buytroupe1p1;
        public InputAction @buytroupe2p1 => m_Wrapper.m_Gameplay_buytroupe2p1;
        public InputAction @opentroupsp1 => m_Wrapper.m_Gameplay_opentroupsp1;
        public InputAction @movep2 => m_Wrapper.m_Gameplay_movep2;
        public InputAction @buytower1p2 => m_Wrapper.m_Gameplay_buytower1p2;
        public InputAction @buytower2p2 => m_Wrapper.m_Gameplay_buytower2p2;
        public InputAction @buytroupe1p2 => m_Wrapper.m_Gameplay_buytroupe1p2;
        public InputAction @buytroupe2p2 => m_Wrapper.m_Gameplay_buytroupe2p2;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @movep1.started += instance.OnMovep1;
            @movep1.performed += instance.OnMovep1;
            @movep1.canceled += instance.OnMovep1;
            @buytower1p1.started += instance.OnBuytower1p1;
            @buytower1p1.performed += instance.OnBuytower1p1;
            @buytower1p1.canceled += instance.OnBuytower1p1;
            @buytower2p1.started += instance.OnBuytower2p1;
            @buytower2p1.performed += instance.OnBuytower2p1;
            @buytower2p1.canceled += instance.OnBuytower2p1;
            @buytroupe1p1.started += instance.OnBuytroupe1p1;
            @buytroupe1p1.performed += instance.OnBuytroupe1p1;
            @buytroupe1p1.canceled += instance.OnBuytroupe1p1;
            @buytroupe2p1.started += instance.OnBuytroupe2p1;
            @buytroupe2p1.performed += instance.OnBuytroupe2p1;
            @buytroupe2p1.canceled += instance.OnBuytroupe2p1;
            @opentroupsp1.started += instance.OnOpentroupsp1;
            @opentroupsp1.performed += instance.OnOpentroupsp1;
            @opentroupsp1.canceled += instance.OnOpentroupsp1;
            @movep2.started += instance.OnMovep2;
            @movep2.performed += instance.OnMovep2;
            @movep2.canceled += instance.OnMovep2;
            @buytower1p2.started += instance.OnBuytower1p2;
            @buytower1p2.performed += instance.OnBuytower1p2;
            @buytower1p2.canceled += instance.OnBuytower1p2;
            @buytower2p2.started += instance.OnBuytower2p2;
            @buytower2p2.performed += instance.OnBuytower2p2;
            @buytower2p2.canceled += instance.OnBuytower2p2;
            @buytroupe1p2.started += instance.OnBuytroupe1p2;
            @buytroupe1p2.performed += instance.OnBuytroupe1p2;
            @buytroupe1p2.canceled += instance.OnBuytroupe1p2;
            @buytroupe2p2.started += instance.OnBuytroupe2p2;
            @buytroupe2p2.performed += instance.OnBuytroupe2p2;
            @buytroupe2p2.canceled += instance.OnBuytroupe2p2;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @movep1.started -= instance.OnMovep1;
            @movep1.performed -= instance.OnMovep1;
            @movep1.canceled -= instance.OnMovep1;
            @buytower1p1.started -= instance.OnBuytower1p1;
            @buytower1p1.performed -= instance.OnBuytower1p1;
            @buytower1p1.canceled -= instance.OnBuytower1p1;
            @buytower2p1.started -= instance.OnBuytower2p1;
            @buytower2p1.performed -= instance.OnBuytower2p1;
            @buytower2p1.canceled -= instance.OnBuytower2p1;
            @buytroupe1p1.started -= instance.OnBuytroupe1p1;
            @buytroupe1p1.performed -= instance.OnBuytroupe1p1;
            @buytroupe1p1.canceled -= instance.OnBuytroupe1p1;
            @buytroupe2p1.started -= instance.OnBuytroupe2p1;
            @buytroupe2p1.performed -= instance.OnBuytroupe2p1;
            @buytroupe2p1.canceled -= instance.OnBuytroupe2p1;
            @opentroupsp1.started -= instance.OnOpentroupsp1;
            @opentroupsp1.performed -= instance.OnOpentroupsp1;
            @opentroupsp1.canceled -= instance.OnOpentroupsp1;
            @movep2.started -= instance.OnMovep2;
            @movep2.performed -= instance.OnMovep2;
            @movep2.canceled -= instance.OnMovep2;
            @buytower1p2.started -= instance.OnBuytower1p2;
            @buytower1p2.performed -= instance.OnBuytower1p2;
            @buytower1p2.canceled -= instance.OnBuytower1p2;
            @buytower2p2.started -= instance.OnBuytower2p2;
            @buytower2p2.performed -= instance.OnBuytower2p2;
            @buytower2p2.canceled -= instance.OnBuytower2p2;
            @buytroupe1p2.started -= instance.OnBuytroupe1p2;
            @buytroupe1p2.performed -= instance.OnBuytroupe1p2;
            @buytroupe1p2.canceled -= instance.OnBuytroupe1p2;
            @buytroupe2p2.started -= instance.OnBuytroupe2p2;
            @buytroupe2p2.performed -= instance.OnBuytroupe2p2;
            @buytroupe2p2.canceled -= instance.OnBuytroupe2p2;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_keyboardSchemeIndex = -1;
    public InputControlScheme keyboardScheme
    {
        get
        {
            if (m_keyboardSchemeIndex == -1) m_keyboardSchemeIndex = asset.FindControlSchemeIndex("keyboard");
            return asset.controlSchemes[m_keyboardSchemeIndex];
        }
    }
    private int m_controllerSchemeIndex = -1;
    public InputControlScheme controllerScheme
    {
        get
        {
            if (m_controllerSchemeIndex == -1) m_controllerSchemeIndex = asset.FindControlSchemeIndex("controller");
            return asset.controlSchemes[m_controllerSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovep1(InputAction.CallbackContext context);
        void OnBuytower1p1(InputAction.CallbackContext context);
        void OnBuytower2p1(InputAction.CallbackContext context);
        void OnBuytroupe1p1(InputAction.CallbackContext context);
        void OnBuytroupe2p1(InputAction.CallbackContext context);
        void OnOpentroupsp1(InputAction.CallbackContext context);
        void OnMovep2(InputAction.CallbackContext context);
        void OnBuytower1p2(InputAction.CallbackContext context);
        void OnBuytower2p2(InputAction.CallbackContext context);
        void OnBuytroupe1p2(InputAction.CallbackContext context);
        void OnBuytroupe2p2(InputAction.CallbackContext context);
    }
}
