using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct CutStoneJob : IJob, IComponentData
    {
        public Vector2Int Position { get; set; }
    }
}
