using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    public struct CarriageJob
    {
        public Building TargetBuilding;
        public int2? SourcePosition;
        public Entity ResourceToCarry;
        public ResourceType ResourceType;
    }
}
