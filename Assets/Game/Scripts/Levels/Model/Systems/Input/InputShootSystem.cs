using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Input;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events.Shoot;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Input
{
    internal sealed class InputShootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<PlayerTagComponent,
                InputComponent,
                ShootingComponent>> _filter = default;

        [Inject] private readonly EventsBus _eventsBus;

        public void Run(IEcsSystems systems)
        {
            if (_eventsBus.HasEventSingleton<InputOnScreenStartedEvent>())
            {
                StartProcessShoot();
            }
            if (_eventsBus.HasEventSingleton<InputOnScreenEndedEvent>())
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
            foreach (var entity in _filter.Value)
            {
                if (value)
                {
                    _eventsBus.NewEventSingleton<ShootStartedEvent>();
                }
                else
                {
                    _eventsBus.NewEventSingleton<ShootEndedEvent>();
                }

                ref var shootingComponent = ref _filter.Get3(entity);
                shootingComponent.IsShooting = value;
            }
        }
    }
}