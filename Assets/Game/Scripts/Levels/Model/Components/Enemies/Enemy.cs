using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfigurationSo _settings;
        public EnemyConfigurationSo Settings => _settings;

        [SerializeField] private Canvas _hpBarCanvas;
        public Canvas HpBarCanvas => _hpBarCanvas;

        [SerializeField] private Image _hpImageFill;
        public Image HpImageFill => _hpImageFill;

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