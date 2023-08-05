using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Components.Behaviours
{
    internal sealed class BehavioursScope : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _behavioursMono;

        public ReadOnlyCollection<IBehaviour> Behaviours { get; private set; }

        private void Awake()
        {
            var temp = new List<IBehaviour>();
            foreach (var behaviour in _behavioursMono)
            {
                if (behaviour is IBehaviour concreteBehaviour)
                {
                    temp.Add(concreteBehaviour);
                }
            }

            Behaviours = new ReadOnlyCollection<IBehaviour>(temp);
        }
    }
}