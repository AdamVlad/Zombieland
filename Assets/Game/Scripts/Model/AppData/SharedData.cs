using Assets.Plugins.IvaLeoEcsLite.EcsEvents;
using UnityEngine;

namespace Assets.Game.Scripts.Model.AppData
{
    internal class SharedData
    {
        public EventsBus EventsBus { get; set; }
        public Camera MainCamera { get; set; }
    }
}