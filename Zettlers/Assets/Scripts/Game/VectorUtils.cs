using Unity.Mathematics;
using UnityEngine;
namespace zettlers
{
    static class VectorUtils
    {
        public static Vector3 ToVector3(this Vector2Int v2)
        {
            return new Vector3(v2.x, 7, v2.y);
        }

        public static Vector2Int ToVector2Int(this float3 v3)
        {
            return new Vector2Int((int)v3.x, (int)v3.z);
        }
    }
}