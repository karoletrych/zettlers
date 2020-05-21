using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct CarriageJob
    {
        public Building TargetBuilding;
        public Vector2Int? SourcePosition;
        public Entity ResourceToCarry;
        public ResourceType ResourceType;
    }
}
