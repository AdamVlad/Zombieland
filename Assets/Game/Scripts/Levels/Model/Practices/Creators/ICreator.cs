using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Creators
{
    internal interface ICreator<out T> where T : MonoBehaviour
    {
        public T CreateNext();
        bool CanCreate();
    }
}