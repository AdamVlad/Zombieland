using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;

using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Enemies
{
    internal sealed class EnemyHpBarActivateSystem : IEcsRunSystem
    {
        [Inject] private readonly EventsBus _eventsBus;
        [Inject] private readonly EcsWorld _world;

        private readonly EcsPoolInject<Executing<HpBarActiveProcess>> _executingProcessPool = default;
        private readonly EcsPoolInject<HpBarActiveProcess> _activateProcessPool = default;
        private readonly EcsPoolInject<Process> _processPool = default;

        private readonly EcsPoolInject<HpBarComponent> _hpPool = default;
        private readonly EcsPoolInject<MonoLink<Enemy>> _enemyPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var eventEntity in _eventsBus.GetEventBodies<GetDamageEvent>(out var getDamageEventPool))
            {
                var hittedEntityPacked = getDamageEventPool.Get(eventEntity).To;

                if (!hittedEntityPacked.Unpack(_world, out var hittedEntity)) continue;
                if (!_enemyPool.Has(hittedEntity)) continue;
                if (!_hpPool.Has(hittedEntity)) continue;

                ref var enemy = ref _enemyPool.Get(hittedEntity).Value;
                ref var hpBarComponent = ref _hpPool.Get(hittedEntity);

                if (hpBarComponent.HpBarCanvas.enabled)
                {
                    ReshowHpBar(hittedEntity, enemy.Settings.HealthBarHideDelay);
                }
                else
                {
                    hpBarComponent.HpBarCanvas.enabled = true;
                    ShowHpBar(hittedEntity, enemy.Settings.HealthBarHideDelay);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReshowHpBar(int targetEntity, float time)
        {
            if (!_executingProcessPool.Has(targetEntity)) return;

            ref var executingProcess = ref _executingProcessPool.Get(targetEntity);

            _processPool.SetDurationToProcess(executingProcess.ProcessEntity, time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ShowHpBar(int targetEntity, float time)
        {
            _activateProcessPool.StartNewProcess(targetEntity, time);
        }
    }
}