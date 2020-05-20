using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct CarryInJob : IComponentData, IJob
    {
        public Guid TargetBuildingId;
        public Vector2Int TargetBuildingPosition;
        public ResourceType ResourceType;
    }
}
