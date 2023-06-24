using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Assets.Plugins.IvaLeoEcsLite.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class InputShootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<InputComponent,
                ShootingComponent>> _shootingFilter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            // по-хорошему тут нужно прокидывать entity, который вызвал событие, и именно для
            // него вызывать соответствующие процессы
            if (eventsBus.HasEventSingleton<InputShootStartedEvent>())
            {
                StartProcessShoot();
            }
            if (eventsBus.HasEventSingleton<InputShootEndedEvent>())
            {
                EndProcessShoot();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void StartProcessShoot()
        {
            ProcessShoot(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EndProcessShoot()
        {
            ProcessShoot(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessShoot(bool value)
        {
            foreach (var entity in _shootingFilter.Value)
            {
                ref var shootingComponent = ref _shootingFilter.Get2(entity);
                shootingComponent.IsShooting = value;
            }
        }
    }
}