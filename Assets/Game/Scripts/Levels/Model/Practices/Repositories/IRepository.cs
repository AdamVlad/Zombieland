using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.Practices.Repositories
{
    internal interface IRepository<out T> where T : MonoBehaviour
    {
        T Get(int index);
        int Count();
    }
}