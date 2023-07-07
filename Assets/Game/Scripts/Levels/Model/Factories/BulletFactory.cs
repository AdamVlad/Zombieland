using Assets.Game.Scripts.Levels.Model.Components.Weapons;
using Assets.Game.Scripts.Levels.Model.Components.Weapons.Charges;
using Assets.Plugins.IvaLib.UnityLib.Factory;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Factories
{
    internal class BulletFactory : IObjectFactory<Bullet, Bullet>
    {
        public Bullet Create(Bullet prefab, Transform parent, Vector3 position = default)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }
    }
}