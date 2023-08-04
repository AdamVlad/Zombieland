using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<ChargeTagComponent,
                MonoLink<Transform>,
                Started<ChargeActiveProcess>>> _filter = default;

        private readonly EcsPoolInject<MonoLink<Transform>> _transformPool = default;
        private readonly EcsPoolInject<WeaponShootingComponent> _weaponShootingPool = default;
        private readonly EcsPoolInject<ChargeActiveProcess> _chargeActiveProcessPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var chargeEntity in _filter.Value)
            {
                var processEntity = _filter.Get3(chargeEntity).ProcessEntity;
                ref var activeProcess = ref _chargeActiveProcessPool.Get(processEntity);

                ref var chargeTransform = ref _filter.Get2(chargeEntity).Value;
                ref var playerTransform = ref _transformPool.Get(activeProcess.PlayerEntity).Value;
                ref var weaponShootingComponent = ref _weaponShootingPool.Get(activeProcess.WeaponEntity);

                chargeTransform.position = weaponShootingComponent.StartShootingPoint.position;
                chargeTransform.DOMove(
                    chargeTransform.position +
                    playerTransform.forward * weaponShootingComponent.ShootingDistance,
                    weaponShootingComponent.ShootingPower);
            }
        }
    }
}