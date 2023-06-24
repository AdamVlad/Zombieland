using UnityEngine;

namespace Assets.Game.Scripts.Model.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "Settings/SceneSettings")]
    internal class SceneConfigurationSo : ScriptableObject
    {
        [SerializeField] private LayerMask _raycastableMask;
        public LayerMask RaycastableMask => _raycastableMask;
    }
}