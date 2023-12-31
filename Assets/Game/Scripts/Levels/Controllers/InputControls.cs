//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Game/Scripts/Levels/Controllers/InputControls.inputactions
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

namespace Assets.Game.Scripts.Controllers
{
    public partial class @InputControls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""9bbe38a5-9ed0-42a3-a147-473b5d9ac473"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b99b1649-5a51-4dce-b68e-ee62c2cccd8d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""8c3e6a9b-aefd-46c8-ac28-6a5f2a255204"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseClickPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8d3d907e-0b11-4a0f-81e9-c09869c4a2a3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""wasd"",
                    ""id"": ""48132633-69a2-43af-ade6-12e77c3bf2e6"",
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
                    ""id"": ""0bc33cbb-597b-4791-9b6a-d2e3bfe846c8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""80edfb7b-6294-42ab-aed0-4a663c9e55c2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""72268435-68ed-4d47-bb56-864dab7f7995"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c4c45ad2-0619-4bb2-8863-82f2de180332"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""903b7d84-7f92-4d19-ba40-4367eb4203a3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fc9bd2f-798e-4a9e-8ba1-9783b7c1f663"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""MouseClickPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // ActionMap
            m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
            m_ActionMap_Move = m_ActionMap.FindAction("Move", throwIfNotFound: true);
            m_ActionMap_Click = m_ActionMap.FindAction("Click", throwIfNotFound: true);
            m_ActionMap_MouseClickPosition = m_ActionMap.FindAction("MouseClickPosition", throwIfNotFound: true);
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

        // ActionMap
        private readonly InputActionMap m_ActionMap;
        private List<IActionMapActions> m_ActionMapActionsCallbackInterfaces = new List<IActionMapActions>();
        private readonly InputAction m_ActionMap_Move;
        private readonly InputAction m_ActionMap_Click;
        private readonly InputAction m_ActionMap_MouseClickPosition;
        public struct ActionMapActions
        {
            private @InputControls m_Wrapper;
            public ActionMapActions(@InputControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_ActionMap_Move;
            public InputAction @Click => m_Wrapper.m_ActionMap_Click;
            public InputAction @MouseClickPosition => m_Wrapper.m_ActionMap_MouseClickPosition;
            public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
            public void AddCallbacks(IActionMapActions instance)
            {
                if (instance == null || m_Wrapper.m_ActionMapActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_ActionMapActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @MouseClickPosition.started += instance.OnMouseClickPosition;
                @MouseClickPosition.performed += instance.OnMouseClickPosition;
                @MouseClickPosition.canceled += instance.OnMouseClickPosition;
            }

            private void UnregisterCallbacks(IActionMapActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Click.started -= instance.OnClick;
                @Click.performed -= instance.OnClick;
                @Click.canceled -= instance.OnClick;
                @MouseClickPosition.started -= instance.OnMouseClickPosition;
                @MouseClickPosition.performed -= instance.OnMouseClickPosition;
                @MouseClickPosition.canceled -= instance.OnMouseClickPosition;
            }

            public void RemoveCallbacks(IActionMapActions instance)
            {
                if (m_Wrapper.m_ActionMapActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IActionMapActions instance)
            {
                foreach (var item in m_Wrapper.m_ActionMapActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_ActionMapActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public ActionMapActions @ActionMap => new ActionMapActions(this);
        private int m_KeyboardAndMouseSchemeIndex = -1;
        public InputControlScheme KeyboardAndMouseScheme
        {
            get
            {
                if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
                return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
            }
        }
        public interface IActionMapActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
            void OnMouseClickPosition(InputAction.CallbackContext context);
        }
    }
}
