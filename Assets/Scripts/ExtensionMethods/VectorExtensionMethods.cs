using UnityEngine;

namespace ExtensionMethods
{
    public static class VectorExtensionMethods
    {
        public static bool IsZero(this Vector2 vector2, float sqrEpsilon = Vector2.kEpsilon)
        {
            return vector2.sqrMagnitude <= sqrEpsilon;
        }

        public static bool IsZero(this Vector3 vector3, float sqrEpsilon = Vector3.kEpsilon)
        {
            return vector3.sqrMagnitude <= sqrEpsilon;
        }
    }
}