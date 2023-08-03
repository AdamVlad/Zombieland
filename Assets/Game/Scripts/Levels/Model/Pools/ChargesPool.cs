using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using Assets.Plugins.IvaLib.UnityLib.Pools;

namespace Assets.Game.Scripts.Levels.Model.Pools
{
    internal class ChargesPool : PoolBase<Charge, IFactory<Charge, Charge>>
    {
        public ChargesPool(
            Charge prefab, 
            int poolSize,
            IFactory<Charge, Charge> factory) : base(prefab, poolSize, factory)
        {
        }
    }
}