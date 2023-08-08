using System.Runtime.CompilerServices;

using Assets.Game.Scripts.Levels.Model.Components.Abilities;
using Assets.Game.Scripts.Levels.Model.Components.Behaviours.Base;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Delayed;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;

using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    [RequireComponent(typeof(Enemy))]
    internal sealed class EnemyAttackBehaviour : CollisionBehaviourBase
    {
        [Inject] private EcsWorld _world;

        [SerializeField] private MonoBehaviour _attackAbilityMono;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();

            Initialize(
                5,
                _enemy.Settings.AttackRadius,
                _enemy.Settings.DetectionMask);

            _attackAbility = _attackAbilityMono as IAbility;

            _attackDelayedTimerPool = _world.GetPool<DelayedRemove<AttackDelayed>>();
            _attackDelayedPool = _world.GetPool<AttackDelayed>();
            _attackPool = _world.GetPool<ShootingComponent>();
        }

        public override float Evaluate()
        {
            var hits = Physics.OverlapSphereNonAlloc(
                transform.position,
                CollisionRadius,
                HittedColliders,
                CollisionLayers); ;

            return hits != 0 ? 0.9f : 0;
        }

        public override void Behave()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return;
            if (_attackDelayedPool.Has(enemyEntity)) return;

            _attackPool.Get(enemyEntity).IsShooting = true;

            _attackAbility?.Execute();

            SetAttackDelayTime(enemyEntity, _enemy.Settings.AttackDelay);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetAttackDelayTime(int targetEntity, float time)
        {
            _attackDelayedPool.Add(targetEntity);

            var delayedEntity = _world.NewEntity();
            ref var timer = ref _attackDelayedTimerPool.Add(delayedEntity);
            timer.TimeLeft = time;
            timer.Target = _world.PackEntity(targetEntity);
        }

        private Enemy _enemy;
        private IAbility _attackAbility;

        private EcsPool<DelayedRemove<AttackDelayed>> _attackDelayedTimerPool;
        private EcsPool<AttackDelayed> _attackDelayedPool;
        private EcsPool<ShootingComponent> _attackPool;
    }
}