using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{

    public struct Building : IComponentData
    {
        public Entity Entity;
        public Guid Id;
        public BuildingType Type;
        public int2 Position;
    }
}
