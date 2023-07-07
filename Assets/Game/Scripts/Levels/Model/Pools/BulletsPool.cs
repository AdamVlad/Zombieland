using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Game.Scripts.Levels.Model.Factories;
using Assets.Plugins.IvaLib.UnityLib.Pools;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Pools
{
    internal class BulletsPool : PoolBase<Bullet, BulletFactory>
    {
        public BulletsPool(
            Bullet prefab, 
            int poolSize,
            BulletFactory factory,
            Transform parent) : base(prefab, poolSize, factory, parent)
        {
        }
    }
}