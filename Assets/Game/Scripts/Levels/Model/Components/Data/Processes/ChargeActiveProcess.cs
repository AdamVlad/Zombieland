using Assets.Plugins.IvaLib.LeoEcsLite.EcsProcess;

namespace Assets.Game.Scripts.Levels.Model.Components.Data.Processes
{
    internal struct ChargeActiveProcess : IProcessData
    {
        public int PlayerEntity;
        public int WeaponEntity;
    }
}