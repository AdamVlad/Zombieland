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
            _inputControls.ActionMap.Move.performed += SetInputAxis;
            _inputControls.ActionMap.Move.canceled += SetInputAxis;

            // Shoot
            _inputControls.ActionMap.Shoot.started += _ => _isShooting = true;
            _inputControls.ActionMap.Shoot.canceled += _ => _isShooting = false;
            _inputControls.ActionMap.MouseClickPosition.started += SetClickPosition;
            _inputControls.ActionMap.MouseClickPosition.performed += SetClickPosition;

            _inputControls.Enable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetInputAxis(InputAction.CallbackContext context)
        {
            _sharedData.Value.EventsBus.NewEventSingleton<InputMoveEvent>() =
                new InputMoveEvent
                {
                    Axis = context.ReadValue<Vector2>()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetClickPosition(InputAction.CallbackContext context)
        {
            if (!_isShooting) return;

            _sharedData.Value.EventsBus.NewEventSingleton<InputShootEvent>() =
                new InputShootEvent
                {
                    ScreenPosition = context.ReadValue<Vector2>()
                };
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputControls.Disable();
        }

        private bool _isShooting;
    }
}