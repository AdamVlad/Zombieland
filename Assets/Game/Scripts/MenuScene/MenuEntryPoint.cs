using UnityEngine;
using System;

using Assets.Game.Scripts.MenuScene.View;
using Assets.Plugins.IvaLib.LeoEcsLite.EcsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;

#region Debug
#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif
#endregion

namespace Assets.Game.Scripts.MenuScene
{
    [Serializable]
    internal struct DebugControls
    {
        public bool IsEcsWorldDebugEnable;
    }

    internal sealed class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private EcsUguiEmitter _uguiEmitter;

        [Space, Header("Debugs")]
        [SerializeField] private DebugControls _debugControls;

        private EcsWorld _world;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;

        private void Awake()
        {
            //Application.targetFrameRate = 60;

            _world = new EcsWorld();

            _initSystems = new EcsSystems(_world);
            _initSystems
                .Add(new SelectedCharacterButtonInitSystem())
                .InjectUgui(_uguiEmitter)
                .Init();

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                .Add(new ChangeHeroButtonClickEventSystem())
            #region Debug Systems
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem(), _debugControls.IsEcsWorldDebugEnable)
                .Add(new EcsSystemsDebugSystem(), _debugControls.IsEcsWorldDebugEnable)

#endif
            #endregion
                .DelHere<EcsUguiClickEvent>()
                .Init();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void OnDestroy()
        {
            _initSystems?.GetWorld("ugui-events")?.Destroy();
            _initSystems?.Destroy();
            _initSystems = null;

            _updateSystems?.Destroy();
            _updateSystems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}