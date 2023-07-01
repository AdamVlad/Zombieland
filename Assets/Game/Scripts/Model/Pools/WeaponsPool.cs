using Assets.Game.Scripts.Model.Components.Items;
using Assets.Game.Scripts.Model.Factories;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsPool;

namespace Assets.Game.Scripts.Model.Pools
{
    internal class WeaponsPool : EcsPoolBase<PoolingObject, WeaponFactory>
    {
        protected override PoolingObject CreatedPooledItem()
        {
            var poolingGameObject = base.CreatedPooledItem();
            poolingGameObject.Pool = Pool;
            return poolingGameObject;
        }
    }
}
