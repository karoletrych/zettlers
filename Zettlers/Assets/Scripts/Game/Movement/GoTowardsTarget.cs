using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    struct GoTowardsTarget : IComponentData
    {
        public int2 TargetPosition;
    }
}