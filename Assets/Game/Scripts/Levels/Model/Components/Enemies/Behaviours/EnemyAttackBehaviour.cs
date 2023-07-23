﻿using Assets.Game.Scripts.Levels.Model.Components.Delayed;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsDelay;
using Leopotam.EcsLite;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies.Behaviours
{
    [RequireComponent(typeof(Enemy))]
    internal sealed class EnemyAttackBehaviour : MonoBehaviour, IBehaviour
    {
        [Inject]
        private void Construct(
            EcsWorld world)
        {
            _world = world;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();

            _attackRadius = _enemy.Settings.AttackRadius;
            _layerMask = _enemy.Settings.DetectionMask;

            _attackDelayedTimerPool = _world.GetPool<DelayedRemove<AttackDelayed>>();
            _attackDelayedPool = _world.GetPool<AttackDelayed>();
        }

        public float Evaluate()
        {
            var hits = Physics.OverlapSphereNonAlloc(
                transform.position,
                _attackRadius,
                _hitColliders,
                _layerMask);

            return hits != 0 ? 0.9f : 0;
        }

        public void Behave()
        {
            if (!_enemy.Unpack(_world, out var enemyEntity)) return;
            if (_attackDelayedPool.Has(enemyEntity)) return;

            Debug.Log("Attack!");

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

        private EcsWorld _world;

        private EcsPool<DelayedRemove<AttackDelayed>> _attackDelayedTimerPool;
        private EcsPool<AttackDelayed> _attackDelayedPool;

        private Enemy _enemy;
        private Collider[] _hitColliders = new Collider[5];
        private float _attackRadius;
        private LayerMask _layerMask;
    }
}