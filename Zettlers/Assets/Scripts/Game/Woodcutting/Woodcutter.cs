using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Woodcutter : IComponentData
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public CutTreeJob? Job { get; set; }
    }
}
