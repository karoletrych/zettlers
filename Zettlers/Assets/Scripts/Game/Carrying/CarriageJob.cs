using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct CarriageJob
    {
        public Guid TargetBuildingId;
        public Vector2Int TargetBuildingPosition;
        public Vector2Int? SourcePosition;
        public Entity ResourceToCarry;
        public ResourceType ResourceType;
    }
}
