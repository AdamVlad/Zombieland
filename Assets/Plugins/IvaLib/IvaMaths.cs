using UnityEngine;

namespace Assets.Plugins.IvaLib
{
    public static class IvaMaths
    {
        public static Vector2 ConvertCoordinatesToForward(ref Vector2 axis)
        {
            return new Vector2(
                0,
                Mathf.Sqrt(axis.x * axis.x + axis.y * axis.y));
        }

        public static Vector2 ConvertCoordinatesRelativeShiftedSystemBy(float angle, ref Vector2 oldPoint)
        {
            var angleOnRadians = angle * Mathf.PI / 180;

            var convertedX =
                oldPoint.x * Mathf.Cos(angleOnRadians) +
                oldPoint.y * Mathf.Sin(angleOnRadians);

            var convertedY =
                oldPoint.y * Mathf.Cos(angleOnRadians) -
                oldPoint.x * Mathf.Sin(angleOnRadians);

            return new Vector2(convertedX, convertedY);
        }

        public static float GetAngleBetween(ref Vector3 fromPoint, ref Vector3 toPoint)
        {
            var from = Quaternion.Euler(fromPoint);
            var to = Quaternion.LookRotation(toPoint);
            var angle = Quaternion.Angle(from, to);

            return toPoint.x > 0 ? 360 - angle : angle;
        }
    }
}