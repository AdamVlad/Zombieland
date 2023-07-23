using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfigurationSo _settings;
        public EnemyConfigurationSo Settings => _settings;

        private EcsPackedEntity _entityPacked;

        public bool Unpack(EcsWorld world, out int unpacked)
        {
            return _entityPacked.Unpack(world, out unpacked);
        }

        public void Pack(EcsWorld world, int entity)
        {
            _entityPacked = world.PackEntity(entity);
        }
    }
}