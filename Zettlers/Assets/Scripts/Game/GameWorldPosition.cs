using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    public struct GameWorldPosition : IComponentData
    {
        public int2 Position;
    }
}