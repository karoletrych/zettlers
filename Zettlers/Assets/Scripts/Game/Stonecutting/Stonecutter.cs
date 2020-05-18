using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Stonecutter : IComponentData
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public CutStoneJob? Job { get; set; }
    }
}
