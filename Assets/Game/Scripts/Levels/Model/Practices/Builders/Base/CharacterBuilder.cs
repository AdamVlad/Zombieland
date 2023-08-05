using Assets.Game.Scripts.Levels.Model.Components.Behaviours;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;
using Assets.Game.Scripts.Levels.Model.Practices.Builders.Context;

using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Practices.Builders.Base
{
    internal abstract class CharacterBuilder<T> : CharacterBuilderBase
        where T : MonoBehaviour
    {
        public CharacterBuilder(EcsContext context) : base(context)
        {
        }

        protected GameObject _character;

        private T _prefab;
        private Vector3 _position;
        private Transform _parent;

        private bool _withTransform;
        private bool _withCollider;
        private bool _withAnimator;
        private bool _withNavMesh;
        private bool _withEntityReference;
        private bool _withBehaviours;
        private bool _withAttack;

        public CharacterBuilder<T> WithPrefab(T prefab)
        {
            _prefab = prefab;
            return this;
        }

        public CharacterBuilder<T> WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public CharacterBuilder<T> WithParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public CharacterBuilder<T> WithTransform()
        {
            _withTransform = true;
            return this;
        }

        public CharacterBuilder<T> WithCollider()
        {
            _withCollider = true;
            return this;
        }

        public CharacterBuilder<T> WithAnimator()
        {
            _withAnimator = true;
            return this;
        }

        public CharacterBuilder<T> WithNavMesh()
        {
            _withNavMesh = true;
            return this;
        }

        public CharacterBuilder<T> WithEntityReference()
        {
            _withEntityReference = true;
            return this;
        }

        public CharacterBuilder<T> WithBehaviours()
        {
            _withBehaviours = true;
            return this;
        }

        public CharacterBuilder<T> WithAttack()
        {
            _withAttack = true;
            return this;
        }

        public T Build(DiContainer _container)
        {
            ThrowExceptionIfPrefabNotSet();

            _character = _container.InstantiatePrefab(
                _prefab,
                _position,
                Quaternion.identity,
                _parent);

            if (_withTransform) _context.SetTransform(_character.transform);
            if (_withCollider) _context.SetCollider(_character.GetComponent<Collider>());
            if (_withAnimator) _context.SetAnimator(_character.GetComponentInChildren<Animator>());
            if (_withNavMesh) _context.SetNavMesh(_character.GetComponent<NavMeshAgent>());
            if (_withBehaviours) _context.SetBehaviours(_character.GetComponent<BehavioursScope>());
            if (_withEntityReference) _context.SetEntityReference(_character.AddComponent<EntityReference>());
            if (_withAttack) _context.SetAttack();

            BuildInternal(_container);

            return _character.GetComponent<T>();
        }

        protected abstract void BuildInternal(DiContainer _container);

        private void ThrowExceptionIfPrefabNotSet()
        {
            if (_prefab == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}