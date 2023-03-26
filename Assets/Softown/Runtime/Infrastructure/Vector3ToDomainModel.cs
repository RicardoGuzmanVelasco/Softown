using Softown.Runtime.Domain.Plotting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Softown.Runtime.Infrastructure
{
    public static class Vector3ToDomainModel
    {
        public static Vector3 To3DWithY(this Foundation tuple, float y)
        {
            return new(tuple.Size.x, y, tuple.Size.y);
        }
        public static Vector3 To3DWithY(this (int x, int y) tuple, float y)
        {
            return new Vector3(tuple.x, y, tuple.y);
        }
        
        public static Foundation ToFoundation(this Vector3 vector)
        {
            Assert.AreApproximatelyEqual((int)vector.x, vector.x);
            Assert.AreApproximatelyEqual((int)vector.z, vector.z);
            
            return Foundation.RectangleOf((int)vector.x, (int)vector.z);
        }
    }
}