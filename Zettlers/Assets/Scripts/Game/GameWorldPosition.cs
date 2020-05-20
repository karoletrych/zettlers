using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct GameWorldPosition : IComponentData
    {
        public Vector2Int Position;
    }
}