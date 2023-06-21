using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components.Events.Input;

namespace Assets.Game.Scripts.Controllers
{
    internal sealed class InputInitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsSharedInject<SharedData> _sharedData = default;

        private InputControls _inputControls;

        public void Init(IEcsSystems systems)
        {
            _inputControls = new InputControls();

            // Move
            _inputControls.ActionMap.Move.performed += OnInputMoveEvent;
            _inputControls.ActionMap.Move.canceled += OnInputMoveEvent;

            // Shoot
            _inputControls.ActionMap.Shoot.started += OnInputShootStartedEvent;
            _inputControls.ActionMap.Shoot.canceled += OnInputShootEndedEvent;

            _inputControls.ActionMap.MouseClickPosition.started += OnInputShootDirectionChangedEvent;
            _inputControls.ActionMap.MouseClickPosition.performed += OnInputShootDirectionChangedEvent;
            _inputControls.ActionMap.MouseClickPosition.canceled += OnInputShootDirectionChangedEvent;

            _inputControls.Enable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputMoveEvent(InputAction.CallbackContext context)
        {
            _sharedData.Value.EventsBus.NewEventSingleton<InputMoveChangedEvent>() =
                new InputMoveChangedEvent
                {
                    Axis = context.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputShootStartedEvent(InputAction.CallbackContext context)
        {
            _isShooting = true;

            _sharedData.Value.EventsBus.NewEventSingleton<InputShootStartedEvent>() =
                new InputShootStartedEvent
                {
                    ScreenPosition = _inputControls.ActionMap.MouseClickPosition.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputShootEndedEvent(InputAction.CallbackContext context)
        {
            _isShooting = false;

            _sharedData.Value.EventsBus.NewEventSingleton<InputShootEndedEvent>() =
                new InputShootEndedEvent
                {
                    ScreenPosition = _inputControls.ActionMap.MouseClickPosition.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnInputShootDirectionChangedEvent(InputAction.CallbackContext context)
        {
            if (!_isShooting) return;

            _sharedData.Value.EventsBus.NewEventSingleton<InputShootDirectionChangedEvent>() =
                new InputShootDirectionChangedEvent
                {
                    ScreenInputPosition = context.ReadValue<Vector2>()
                };
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputControls.Disable();
        }

        private bool _isShooting;
    }
}