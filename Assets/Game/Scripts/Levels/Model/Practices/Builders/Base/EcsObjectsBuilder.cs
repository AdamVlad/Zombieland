using Assets.Game.Scripts.Levels.Model.Components.Behaviours;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Base
{
    internal abstract class EcsObjectsBuilder<T> : EcsObjectsBuilderBase
        where T : MonoBehaviour
    {
        protected EcsObjectsBuilder(EcsContext context) : base(context)
        {
        }

        protected GameObject ObjectGo;

        private T _prefab;
        private Vector3 _position;
        private Transform _parent;

        private bool _withTransform;
        private bool _withCollider;
        private bool _withRigidbody;
        private bool _withAnimator;
        private bool _withNavMesh;
        private bool _withEntityReference;
        private bool _withBehaviours;
        private bool _withAttack;
        private bool _withParent;
        private bool _withShooting;

        public EcsObjectsBuilder<T> WithPrefab(T prefab)
        {
            _prefab = prefab;
            return this;
        }

        public EcsObjectsBuilder<T> WithPositionInitialize(Vector3 position)
        {
            _position = position;
            return this;
        }

        public EcsObjectsBuilder<T> WithParentInitialize(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public EcsObjectsBuilder<T> WithTransform()
        {
            _withTransform = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithCollider()
        {
            _withCollider = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithRigidbody()
        {
            _withRigidbody = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithAnimator()
        {
            _withAnimator = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithNavMesh()
        {
            _withNavMesh = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithEntityReference()
        {
            _withEntityReference = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithParent()
        {
            _withParent = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithBehaviours()
        {
            _withBehaviours = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithAttack()
        {
            _withAttack = true;
            return this;
        }

        public EcsObjectsBuilder<T> WithShooting()
        {
            _withShooting = true;
            return this;
        }

        public T Build(DiContainer container)
        {
            ThrowExceptionIfPrefabNotSet();

            ObjectGo = container.InstantiatePrefab(
                _prefab,
                _position,
                Quaternion.identity,
                _parent);

            return BuildExplicit();
        }

        public T Build()
        {
            ThrowExceptionIfPrefabNotSet();

            ObjectGo = Object.Instantiate(
                _prefab,
                _position,
                Quaternion.identity,
                _parent).gameObject;

            return BuildExplicit();
        }

        private T BuildExplicit()
        {
            Context.SetNewEntity();

            if (_withTransform) Context.SetTransform(ObjectGo.transform);
            if (_withCollider) Context.SetCollider(ObjectGo.GetComponent<Collider>());
            if (_withRigidbody) Context.SetRigidbody(ObjectGo.GetComponent<Rigidbody>());
            if (_withAnimator) Context.SetAnimator(ObjectGo.GetComponentInChildren<Animator>());
            if (_withNavMesh) Context.SetNavMesh(ObjectGo.GetComponent<NavMeshAgent>());
            if (_withBehaviours) Context.SetBehaviours(ObjectGo.GetComponent<BehavioursScope>());
            if (_withEntityReference) Context.SetEntityReference(ObjectGo.GetComponent<EntityReference>());
            if (_withParent) Context.SetParent(_parent);
            if (_withAttack) Context.SetAttack();
            if (_withShooting) Context.SetShooting();

            BuildInternal();

            var result = ObjectGo.GetComponent<T>();

            Reset();

            return result;
        }

        private void Reset()
        {
            ResetInternal();

            _withTransform = false;
            _withCollider = false;
            _withRigidbody = false;
            _withAnimator = false;
            _withNavMesh = false;
            _withBehaviours = false;
            _withEntityReference = false;
            _withParent = false;
            _withAttack = false;
            _withShooting = false;

            ObjectGo = null;
            _prefab = null;
            _position = default;
            _parent = null;
        }

        protected abstract void BuildInternal();
        protected abstract void ResetInternal();

        private void ThrowExceptionIfPrefabNotSet()
        {
            if (_prefab == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}