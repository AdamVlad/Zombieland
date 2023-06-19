using UnityEngine;

namespace Assets.Plugins.IvaLib
{
    public static class ScreenPointToWorldConverter
    {
        public static bool GetWorldPointFrom(
            ref Vector2 screenPoint,
            Camera camera,
            LayerMask raycastableMask,
            out Vector3 result)
        {
            if (Physics.Raycast(
                    camera.ScreenPointToRay(screenPoint),
                    out var hit,
                    100,
                    raycastableMask))
            {
                result = hit.point;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}