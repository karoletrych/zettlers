using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct CarryInJob : IComponentData, IJob
    {
        public Guid TargetBuildingId { get; set; }
        public Vector2Int TargetBuildingPosition { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
