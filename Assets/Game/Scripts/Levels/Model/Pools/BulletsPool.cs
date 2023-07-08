using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.LeoEcsLite.Pools;
using Leopotam.EcsLite;

namespace Assets.Game.Scripts.Levels.Model.Pools
{
    internal class BulletsPool : EcsPoolBase<Bullet, IEcsFactory<Bullet, Bullet>>
    {
        public BulletsPool(
            Bullet prefab, 
            int poolSize,
            IEcsFactory<Bullet, Bullet> factory,
            EcsWorld world) : base(prefab, poolSize, factory, world)
        {
        }
    }
}