using System;
using Leopotam.EcsLite;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess
{
    public interface IProcessData
    {
    }

    [Serializable]
    public struct Process : IStateData
    {
        public StatePhase Phase { get; set; }
        public float Duration;
        public bool Paused;
        public EcsPackedEntity Target;
    }

    public readonly struct Completed<TProcess> where TProcess : struct
    {
        public readonly int ProcessEntity;

        public Completed(int processEntity)
        {
            ProcessEntity = processEntity;
        }
    }

    public readonly struct Executing<TProcess> where TProcess : struct
    {
        public readonly int ProcessEntity;

        public Executing(int processEntity)
        {
            ProcessEntity = processEntity;
        }
    }

    public readonly struct Started<TProcess> where TProcess : struct
    {
        public readonly int ProcessEntity;

        public Started(int processEntity)
        {
            ProcessEntity = processEntity;
        }
    }
}