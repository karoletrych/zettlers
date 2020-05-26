using Unity.Mathematics;
using UnityEngine;
namespace zettlers
{
    public static class VectorUtils
    {
        public static Vector3 ToVector3(this Vector2Int v2)
        {
            return new Vector3(v2.x, 1, v2.y);
        }

        public static float3 ToFloat3(this int2 v2)
        {
            return new float3(v2.x, 1, v2.y);
        }
        public static int2 ToInt2(this float3 v3)
        {
            return new int2((int)v3.x, (int)v3.z);
        }

        public static int2 ToInt2(this Vector3 v3)
        {
            return new int2((int)v3.x, (int)v3.z);
        }

        public static Vector2Int ToVector2Int(this Vector3 v3)
        {
            return new Vector2Int((int)v3.x, (int)v3.z);
        }

        public static Vector2Int ToVector2Int(this float3 v3)
        {
            return new Vector2Int((int)v3.x, (int)v3.z);
        }
    }
}