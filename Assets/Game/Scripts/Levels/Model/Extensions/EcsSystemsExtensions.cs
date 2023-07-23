using Leopotam.EcsLite;
using Zenject;

namespace Assets.Game.Scripts.Levels.Model.Extensions
{
    internal static class EcsSystemsExtensions
    {
        public static IEcsSystems Add<T>(this IEcsSystems systems, DiContainer container)
            where T : IEcsSystem
        {
            container.Bind<T>().AsSingle();
            return systems.Add(container.Resolve<T>());
        }
    }
}