using System;

namespace Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess
{
    public interface IStateData
    {
        public StatePhase Phase { get; set; }
    }

    [Serializable]
    public enum StatePhase
    {
        OnStart,
        Process,
        Complete
    }
}