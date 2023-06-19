using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Plugins.IvaLeoEcsLite.UnityEcsComponents.EntityReference
{
    public sealed class EntityReferenceInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<MonoLink<EntityReference>>> _filter = default;

        private readonly EcsPoolInject<MonoLink<EntityReference>> _entityPool = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var entityReference = ref _entityPool.Value.Get(entity);
                entityReference.Value.Pack(entity);
            }
        }
    }
}