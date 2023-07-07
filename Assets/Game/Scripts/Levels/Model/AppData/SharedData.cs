using Assets.Plugins.IvaLib.LeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Levels.Model.AppData
{
    internal class SharedData
    {
        public EventsBus EventsBus { get; set; }
        public Camera MainCamera { get; set; }
    }
}