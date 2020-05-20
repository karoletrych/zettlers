using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct GoTowardsTarget : IComponentData
    {
        public Vector2Int TargetPosition;
    }
}