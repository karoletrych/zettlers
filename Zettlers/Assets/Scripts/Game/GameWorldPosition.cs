using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct GameWorldPosition : IComponentData
    {
        public Vector2Int Position;
    }
}