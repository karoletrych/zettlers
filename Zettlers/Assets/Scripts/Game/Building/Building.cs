using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Building : IComponentData
    {
        public Guid Id { get; set; }
        public BuildingType Type { get; set; }
        public Vector2Int Position { get; set; }
    }
}
