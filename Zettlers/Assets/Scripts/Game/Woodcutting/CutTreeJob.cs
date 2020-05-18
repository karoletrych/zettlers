using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct CutTreeJob : IJob, IComponentData
    {
        public Vector2Int Position { get; set; }
    }
}
