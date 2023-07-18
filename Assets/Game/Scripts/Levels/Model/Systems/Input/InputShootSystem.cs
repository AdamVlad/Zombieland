using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.AppData;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Events.Input;
using Assets.Game.Scripts.Levels.Model.Components.Events.Shoot;
using Assets.Game.Scripts.Levels.Model.Components.Player;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Levels.Model.Systems.Input
{
    internal sealed class InputShootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<PlayerTagComponent,
                InputComponent,
                ShootingComponent,
                BackpackComponent>> _filter = default;

        private readonly EcsSharedInject<SharedData> _sharedData = default;

        public void Run(IEcsSystems systems)
        {
            var eventsBus = _sharedData.Value.EventsBus;

            // по-хорошему тут нужно прокидывать entity, который вызвал событие, и именно для
            // него вызывать соответствующие процессы
            if (eventsBus.HasEventSingleton<InputOnScreenStartedEvent>())
            {
                StartProcessShoot();
            }
            if (eventsBus.HasEventSingleton<InputOnScreenEndedEvent>())
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
                ref var backpackComponent = ref _filter.Get4(entity);

                // Если оружие не в руках, то игрок не может начать стрелять.
                // При этом дополнительное условие Value нужно для того, чтобы 
                // была возможность перестать стрелять, если оружие не в руках
                if (!backpackComponent.IsWeaponInHand && value) continue;

                if (value)
                {
                    _sharedData.Value.EventsBus.NewEventSingleton<ShootStartedEvent>();
                }
                else
                {
                    _sharedData.Value.EventsBus.NewEventSingleton<ShootEndedEvent>();
                }

                ref var shootingComponent = ref _filter.Get3(entity);
                shootingComponent.IsShooting = value;
            }
        }
    }
}