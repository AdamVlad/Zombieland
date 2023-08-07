using Assets.Game.Scripts.Levels.Model.Components.Data.Enemies;
using Assets.Game.Scripts.Levels.Model.Components.Data.Events;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using Assets.Plugins.IvaLib.LeoEcsLite.UnityEcsComponents.EntityReference;

using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Components.Abilities
{
    [RequireComponent(typeof(Enemy))]
    internal class AttackAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float _attackThroughTime;
        [SerializeField] private float _damagingRadius;
        [SerializeField] private Transform _damagingPart;

        [Inject] private readonly EventsBus _eventsBus;

        private float _time;

        public void Execute()
        {
            StartCoroutine(AttackCoroutine());
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnDisable()
        {
            StopCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            _time = _attackThroughTime;
            while (_time >= 0)
            {
                _time -= Time.deltaTime;
                yield return null;
            }

            var count = Physics.OverlapSphereNonAlloc(
                _damagingPart.position,
                _damagingRadius,
                _hitted,
                _enemy.Settings.DetectionMask);

            if (count <= 0) yield break;

            if (_hitted[0].TryGetComponent<EntityReference>(out var entityReference))
            {
                _eventsBus.NewEvent<GetDamageEvent>()
                    = new GetDamageEvent
                    {
                        From = _enemy.EntityPacked,
                        To = entityReference.EntityPacked,
                        Damage = _enemy.Settings.Damage
                    };
            }
        }

        private readonly Collider[] _hitted = new Collider[10];
        private Enemy _enemy;
    }
}