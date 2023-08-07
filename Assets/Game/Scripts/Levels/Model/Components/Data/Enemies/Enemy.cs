using Assets.Game.Scripts.Levels.Model.ScriptableObjects;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;

using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Enemies
{
    [RequireComponent(typeof(EntityReference))]
    internal class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfigurationSo _settings;
        public EnemyConfigurationSo Settings => _settings;

        [SerializeField] private Canvas _hpBarCanvas;
        public Canvas HpBarCanvas => _hpBarCanvas;

        [SerializeField] private Image _hpImageFill;
        public Image HpImageFill => _hpImageFill;

        private EcsPackedEntity _entityPacked;
        public EcsPackedEntity EntityPacked => _entityPacked;

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