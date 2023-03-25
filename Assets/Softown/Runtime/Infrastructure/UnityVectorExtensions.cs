using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public static class UnityVectorExtensions
    {
        public static Vector2 XZ(this Vector3 vector3) => new(vector3.x, vector3.z);
    }
}