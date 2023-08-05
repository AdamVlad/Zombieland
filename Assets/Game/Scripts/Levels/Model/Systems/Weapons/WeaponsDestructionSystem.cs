using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.Model.Services;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Systems.Weapons
{
    internal sealed class WeaponsDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<DestructionDelayed,
                MonoLink<Weapon>>> _filter = default;

        private readonly EcsPoolInject<DestructionDelayed> _destructionPool = default;

        [Inject] private readonly WeaponsProviderService _weaponsService;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _weaponsService.Return(_filter.Get2(entity).Value);

                _destructionPool.Del(entity);
            }
        }
    }
}