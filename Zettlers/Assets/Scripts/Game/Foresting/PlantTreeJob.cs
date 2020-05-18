using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct PlantTreeJob : IJob, IComponentData
    {
        public Vector2Int TargetPosition { get; set; }
    }
}
