using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Assets.Game.Scripts.Model.Extensions
{
    public static class EcsSystemGroupExtensions
    {
        public static List<IEcsSystem> AddToGroup(this List<IEcsSystem> systemGroup, IEcsSystem system)
        {
            systemGroup.Add(system);
            return systemGroup;
        }

        public static List<IEcsSystem> DelHere<TComponent>(this List<IEcsSystem> systemGroup, EcsWorld world) where TComponent : struct
        {
            systemGroup.Add(new DelHereSystem<TComponent>(world));
            return systemGroup;
        }
    }
}