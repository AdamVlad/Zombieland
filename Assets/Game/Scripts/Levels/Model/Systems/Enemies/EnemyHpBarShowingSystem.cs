using Assets.Game.Scripts.Levels.Model.Components.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Levels.Model.Components;
using Assets.Game.Scripts.Levels.Model.Components.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    // Подумать, как можно вынести систему Lifetime
    internal sealed class EnemyHpBarShowingSystem : IEcsRunSystem
    {
        [Inject] private EventsBus _eventsBus;

        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool = default;
        private readonly EcsPoolInject<HpBarComponent> _hpPool = default;
        private readonly EcsPoolInject<MonoLink<Enemy>> _enemyPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamageEventPool))
            {
                var getDamageEvent = getDamageEventPool.Get(eventEntity);

                if (!_enemyPool.Has(getDamageEvent.To)) continue;
                if (!_hpPool.Has(getDamageEvent.To)) continue;

                ref var enemy = ref _enemyPool.Get(getDamageEvent.To).Value;
                ref var hpBarComponent = ref _hpPool.Get(getDamageEvent.To);

                if (hpBarComponent.HpBarCanvas.enabled)
                {
                    ResetHpBarHideDelayTime(getDamageEvent.To, enemy.Settings.HealthBarHideDelay);
                }
                else
                {
                    hpBarComponent.HpBarCanvas.enabled = true;
                    SetHpBarHideDelayTime(getDamageEvent.To, enemy.Settings.HealthBarHideDelay);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResetHpBarHideDelayTime(int targetEntity, float time)
        {
            if (!_lifetimePool.Has(targetEntity)) return;

            _lifetimePool.Get(targetEntity).PassedTime = time;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetHpBarHideDelayTime(int targetEntity, float time)
        {
            if (!_lifetimePool.Has(targetEntity))
            {
                ref var lifetimeComponent = ref _lifetimePool.Add(targetEntity);
                lifetimeComponent.Lifetime = time;
                lifetimeComponent.PassedTime = time;
            }
        }
    }
}