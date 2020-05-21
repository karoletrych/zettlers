using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct CarryJob
    {
        public Guid TargetBuildingId;
        public Vector2Int TargetBuildingPosition;
        public Vector2Int? SourcePosition;
        public ResourceType ResourceType;
    }
}
