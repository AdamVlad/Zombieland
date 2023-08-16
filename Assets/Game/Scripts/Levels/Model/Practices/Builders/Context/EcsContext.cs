using Assets.Game.Scripts.Levels.Model.Components.Behaviours;
using Assets.Game.Scripts.Levels.Model.Components.Data;
using Assets.Game.Scripts.Levels.Model.Components.Data.Charges;
using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Player;
using Assets.Game.Scripts.Levels.Model.Components.Data.Weapons;
using Assets.Game.Scripts.Levels.View.Widgets;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Context
{
    internal class EcsContext
    {
        public EcsContext(EcsWorld world)
        {
            _world = world;
        }

        public EcsContext SetNewEntity()
        {
            _entity = _world.NewEntity();
            return this;
        }

        public EcsContext SetTransform(Transform transform)
        {
            var transformPool = _world.GetPool<MonoLink<Transform>>();
            ref var transformComponent = ref transformPool.Add(_entity);
            transformComponent.Value = transform;

            return this;
        }

        public EcsContext SetCollider(Collider collider)
        {
            var colliderPool = _world.GetPool<MonoLink<Collider>>();
            ref var colliderComponent = ref colliderPool.Add(_entity);
            colliderComponent.Value = collider;

            return this;
        }

        public EcsContext SetRigidbody(Rigidbody rigidbody)
        {
            var rigidbodyPool = _world.GetPool<MonoLink<Rigidbody>>();
            ref var rigidbodyComponent = ref rigidbodyPool.Add(_entity);
            rigidbodyComponent.Value = rigidbody;

            return this;
        }

        public EcsContext SetAnimator(Animator animator)
        {
            var animationPool = _world.GetPool<MonoLink<Animator>>();
            ref var animatorComponent = ref animationPool.Add(_entity);
            animatorComponent.Value = animator;

            return this;
        }

        public EcsContext SetNavMesh(NavMeshAgent navMesh)
        {
            var navMeshPool = _world.GetPool<MonoLink<NavMeshAgent>>();
            ref var navMeshAgentComponent = ref navMeshPool.Add(_entity);
            navMeshAgentComponent.Value = navMesh;

            return this;
        }

        public EcsContext SetEntityReference(EntityReference entityReference)
        {
            var entityReferencePool = _world.GetPool<MonoLink<EntityReference>>();
            ref var entityReferenceComponent = ref entityReferencePool.Add(_entity);
            entityReference.Pack(_entity);
            entityReferenceComponent.Value = entityReference;

            return this;
        }

        public EcsContext SetBehaviours(BehavioursScope behaviourScope)
        {
            var behaviourPool = _world.GetPool<BehaviourComponent>();
            behaviourPool.Add(_entity);
            var behavioursScopePool = _world.GetPool<MonoLink<BehavioursScope>>();
            ref var behaviours = ref behavioursScopePool.Add(_entity);
            behaviours.Value = behaviourScope;

            return this;
        }

        public EcsContext SetAttack()
        {
            var attackPool = _world.GetPool<ShootingComponent>();
            attackPool.Add(_entity);

            return this;
        }

        public EcsContext SetHealth(float value)
        {
            var healthPool = _world.GetPool<HealthComponent>();
            ref var healthComponent = ref healthPool.Add(_entity);
            healthComponent.MaxHealth = value;
            healthComponent.CurrentHealth = value;

            return this;
        }

        public EcsContext SetEnemy(Enemy enemy)
        {
            enemy.Pack(_world, _entity);
            var enemyPool = _world.GetPool<MonoLink<Enemy>>();
            ref var enemyComponent = ref enemyPool.Add(_entity);
            enemyComponent.Value = enemy;

            return this;
        }

        public EcsContext SetEnemyTag()
        {
            var enemyTagPool = _world.GetPool<EnemyTagComponent>();
            enemyTagPool.Add(_entity);

            return this;
        }

        public EcsContext SetEnemyHpBar(EnemyHpWidget hpWidget, bool isEnabledOnStart)
        {
            hpWidget.OnInit(1, _world);
            hpWidget.BindWidget(_world, _entity);
            hpWidget.gameObject.SetActive(isEnabledOnStart);

            return this;
        }

        public EcsContext SetDamage(int damage = default)
        {
            var damagePool = _world.GetPool<DamageComponent>();
            damagePool.Add(_entity) = new DamageComponent
            {
                Damage = damage
            };

            return this;
        }

        public EcsContext SetLifetime(float lifetime)
        {
            var lifetimePool = _world.GetPool<LifetimeComponent>();
            ref var lifetimeComponent = ref lifetimePool.Add(_entity);
            lifetimeComponent.Lifetime = lifetime;

            return this;
        }

        public EcsContext SetCharge(Charge charge)
        {
            var chargePool = _world.GetPool<MonoLink<Charge>>();
            ref var chargeComponent = ref chargePool.Add(_entity);
            chargeComponent.Value = charge;
            chargeComponent.Value.Entity = _entity;

            return this;
        }

        public EcsContext SetChargeTag()
        {
            var chargesPool = _world.GetPool<ChargeTagComponent>();
            chargesPool.Add(_entity);

            return this;
        }

        public EcsContext SetWeapon(Weapon weapon)
        {
            var weaponPool = _world.GetPool<MonoLink<Weapon>>();
            ref var weaponComponent = ref weaponPool.Add(_entity);
            weaponComponent.Value = weapon;

            return this;
        }

        public EcsContext SetWeaponClip(int capacity, IObjectPool<Charge> pool)
        {
            var weaponClipPool = _world.GetPool<WeaponClipComponent>();
            weaponClipPool.Add(_entity) = new WeaponClipComponent(
                capacity,
                pool);

            return this;
        }

        public EcsContext SetAttackDelay(float delay)
        {
            var attackDelayPool = _world.GetPool<AttackDelayComponent>();
            attackDelayPool.Add(_entity) = new AttackDelayComponent
            {
                Delay = delay
            };

            return this;
        }

        public EcsContext SetReloadingDelay(float delay)
        {
            var reloadingPool = _world.GetPool<ReloadingComponent>();
            reloadingPool.Add(_entity) = new ReloadingComponent
            {
                Delay = delay
            };

            return this;
        }

        public EcsContext SetWeaponShooting(
            Transform startShootingPoint,
            float shootingDistance,
            float shootingPower)
        {
            var shootPointPool = _world.GetPool<WeaponShootingComponent>();
            shootPointPool.Add(_entity) = new WeaponShootingComponent
            {
                StartShootingPoint = startShootingPoint,
                ShootingDistance = shootingDistance,
                ShootingPower = shootingPower
            };

            return this;
        }

        public EcsContext SetPlayerMove(float speed)
        {
            var movePool = _world.GetPool<PlayerMoveComponent>();
            movePool.Add(_entity) = new PlayerMoveComponent
            {
                Speed = speed
            };

            return this;
        }

        public EcsContext SetPlayerRotation(float speed, float turnAngle)
        {
            var rotationPool = _world.GetPool<RotationComponent>();
            rotationPool.Add(_entity) = new RotationComponent
            {
                Speed = speed,
                SmoothTurningAngle = turnAngle
            };

            return this;
        }

        public EcsContext SetInput()
        {
            var inputPool = _world.GetPool<InputComponent>();
            inputPool.Add(_entity);

            return this;
        }

        public EcsContext SetShooting()
        {
            var shootingPool = _world.GetPool<ShootingComponent>();
            shootingPool.Add(_entity);

            return this;
        }

        public EcsContext SetBackpack(int weaponEntity, Transform weaponHolderPoint)
        {
            var backpackPool = _world.GetPool<BackpackComponent>();
            backpackPool.Add(_entity) = new BackpackComponent
            {
                WeaponEntity = weaponEntity,
                WeaponHolderPoint = weaponHolderPoint
            };

            return this;
        }

        public EcsContext SetPlayerTag()
        {
            var playerPool = _world.GetPool<PlayerTagComponent>();
            playerPool.Add(_entity);

            return this;
        }

        public EcsContext SetPlayer(Player player)
        {
            var playerPool = _world.GetPool<MonoLink<Player>>();
            ref var playerComponent = ref playerPool.Add(_entity);
            playerComponent.Value = player;

            return this;
        }

        public EcsContext ConnectToPlayer()
        {
            var filter = _world.Filter<PlayerTagComponent>().End();
            if (filter.GetFirstEntity(out var playerEntity))
            {
                var backpackPool = _world.GetPool<BackpackComponent>();
                backpackPool.Get(playerEntity).WeaponEntity = _entity;
            }

            return this;
        }

        private readonly EcsWorld _world;
        private int _entity;
    }
}