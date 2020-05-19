using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct Building : IComponentData
    {
        public Guid Id;
        public BuildingType Type;
        public Vector2Int Position;
    }
}
