using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Practices.Builders;

using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Practices.Factories
{
    internal class PlayerFactory : Plugins.IvaLib.UnityLib.Factory.IFactory<Player, Player>
    {
        [Inject] private readonly PlayerBuilder _builder;

        public PlayerFactory(Transform parent = null)
        {
            _parent = parent;
        }

        public Player Create(Player prefab, Vector3 position = default)
        {
            return _builder
                .WithInput()
                .WithHealth()
                .WithMove()
                .WithRotation()
                .WithBackpack()
                .WithTag()
                .WithPlayer()
                .WithPrefab(prefab)
                .WithParentInitialize(_parent)
                .WithPositionInitialize(position)
                .WithTransform()
                .WithRigidbody()
                .WithEntityReference()
                .WithAnimator()
                .WithShooting()

                .Build();
        }

        private readonly Transform _parent;
    }
}