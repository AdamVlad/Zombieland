using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Game.Scripts.Levels.Model.Components.Enemies.Behaviours;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Enemies
{
    internal sealed class BehavioursScope : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _behavioursMono;

        private ReadOnlyCollection<IBehaviour> _behaviours;
        public ReadOnlyCollection<IBehaviour> Behaviours => _behaviours;

        private void Start()
        {
            var temp = new List<IBehaviour>();
            foreach (var behaviour in _behavioursMono)
            {
                if (behaviour is IBehaviour concreteBehaviour)
                {
                    temp.Add(concreteBehaviour);
                }
            }

            _behaviours = new ReadOnlyCollection<IBehaviour>(temp);
        }
    }
}