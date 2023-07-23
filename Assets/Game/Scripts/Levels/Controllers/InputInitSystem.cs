using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Game.Scripts.Levels.Controllers
{
    internal sealed class InputInitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        [Inject] private EventsBus _eventsBus;

        private InputControls _inputControls;

        public void Init(IEcsSystems systems)
        {
            _inputControls = new InputControls();

            // Move
            _inputControls.ActionMap.Move.performed += OnInputMoveEvent;
            _inputControls.ActionMap.Move.canceled += OnInputMoveEvent;

            // Mouse click or touch on screen
            _inputControls.ActionMap.Click.started += OnInputOnScreenStartedEvent;
            _inputControls.ActionMap.Click.canceled += OnInputOnScreenEndedEvent;

            _inputControls.ActionMap.MouseClickPosition.started += OnInputOnScreenPositionChangedEvent;
            _inputControls.ActionMap.MouseClickPosition.performed += OnInputOnScreenPositionChangedEvent;
            _inputControls.ActionMap.MouseClickPosition.canceled += OnInputOnScreenPositionChangedEvent;

            _inputControls.Enable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputMoveEvent(InputAction.CallbackContext context)
        {
            _eventsBus.NewEventSingleton<InputMoveChangedEvent>() =
                new InputMoveChangedEvent
                {
                    Axis = context.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputOnScreenStartedEvent(InputAction.CallbackContext context)
        {
            _isClickedOrTouched = true;

            _eventsBus.NewEventSingleton<InputOnScreenStartedEvent>() =
                new InputOnScreenStartedEvent
                {
                    ScreenPosition = _inputControls.ActionMap.MouseClickPosition.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputOnScreenEndedEvent(InputAction.CallbackContext context)
        {
            _isClickedOrTouched = false;

            _eventsBus.NewEventSingleton<InputOnScreenEndedEvent>() =
                new InputOnScreenEndedEvent
                {
                    ScreenPosition = _inputControls.ActionMap.MouseClickPosition.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputOnScreenPositionChangedEvent(InputAction.CallbackContext context)
        {
            if (!_isClickedOrTouched) return;

            _eventsBus.NewEventSingleton<InputOnScreenPositionChangedEvent>() =
                new InputOnScreenPositionChangedEvent
                {
                    ScreenInputPosition = context.ReadValue<Vector2>()
                };
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputControls.Disable();
        }

        private bool _isClickedOrTouched;
    }
}