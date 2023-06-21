using System.Runtime.CompilerServices;
using Assets.Game.Scripts.Model.AppData;
using Assets.Game.Scripts.Model.Components;
using Assets.Game.Scripts.Model.Components.Events.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Game.Scripts.Model.Systems.Input
{
    internal sealed class ScreenEntityInitSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<InputComponent, ShootingComponent>> _shootingFilter = default;


        public void Init(IEcsSystems systems)
        {
            
        }
    }
}