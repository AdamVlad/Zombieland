using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsFactory;
using Assets.Plugins.IvaLib.LeoEcsLite.Pools;
using Leopotam.EcsLite;

namespace Assets.Game.Scripts.Levels.Model.Pools
{
    internal class ChargesPool : EcsPoolBase<Charge, IEcsFactory<Charge, Charge>>
    {
        public ChargesPool(
            Charge prefab, 
            int poolSize,
            IEcsFactory<Charge, Charge> factory,
            EcsWorld world) : base(prefab, poolSize, factory, world)
        {
        }
    }
}