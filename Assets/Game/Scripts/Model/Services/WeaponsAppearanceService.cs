using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Game.Scripts.Model.Services
{
    internal class WeaponsAppearanceService
    {
        private enum Priority
        {
            None = 0,
            Low = 2,
            Medium = 4,
            High = 8
        }

        private class PriorityComponent
        {
            public Priority Value;
        }

        public WeaponsAppearanceService(ICreator<GameObject> creator)
        {
            _creator = creator;
            _random = new Random();
        }

        public void Run()
        {
            while (_creator.CanCreate())
            {
                var created = _creator.CreateNext();
                created.gameObject.SetActive(false);

                _pool.Add(
                    created,
                    new PriorityComponent{ Value = Priority.High});
            }
        }

        public GameObject Get(Transform spawnPoint)
        {
            var result = GetInternal(Priority.High);
            if (result == null) result = GetInternal(Priority.Medium);
            if (result == null) result = GetInternal(Priority.Low);
            if (result == null) return null;

            if (_pool.TryGetValue(result, out var priority))
            {
                priority.Value = Priority.None;
            }

            result.transform.position = spawnPoint.position;
            result.SetActive(true);

            return result;
        }

        public void Return(GameObject weapon)
        {
            if (_pool.TryGetValue(weapon, out var priority))
            {
                priority.Value = Priority.Low;
            }
            weapon.SetActive(false);

            UpdatePrioritiesIfNeedIt();
        }

        private GameObject GetInternal(Priority priority)
        {
            var selected = _pool
                .Where(p => p.Value.Value == priority)
                .Select(p => p.Key)
                .ToList();

            if (selected.Count == 0) return null;

            var randomIndex = _random.Next(0, selected.Count);
            return selected[randomIndex];
        }

        private void UpdatePrioritiesIfNeedIt()
        {
            _interations++;
            if (_pool.Count != 0 &&
                (float)_interations / _pool.Count >= 0.2f)
            {
                UpdatePriorities();
            }
        }

        private void UpdatePriorities()
        {
            foreach (var priority in _pool.Values)
            {
                priority.Value = priority.Value switch
                {
                    Priority.Low => Priority.Medium,
                    Priority.Medium => Priority.High,
                    _ => priority.Value
                };
            }
        }

        private readonly Dictionary<GameObject, PriorityComponent> _pool = new();
        private readonly ICreator<GameObject> _creator;
        private readonly Random _random;
        private int _interations;
    }
}