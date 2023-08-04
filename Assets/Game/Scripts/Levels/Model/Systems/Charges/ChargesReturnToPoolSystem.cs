using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Processes;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Charges
{
    internal sealed class ChargesReturnToPoolSystem : IEcsRunSystem
    {
        [Inject] private ChargesProviderService _chargesService;

        private readonly EcsFilterInject
            <Inc<ChargeTagComponent,
                MonoLink<Charge>,
                Completed<ChargeActiveProcess>>> _filter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var charge = ref _filter.Get2(entity).Value;

                DOTween.Kill(charge.transform);

                _chargesService.GetPool(charge.Type).Release(charge);
            }
        }
    }
}