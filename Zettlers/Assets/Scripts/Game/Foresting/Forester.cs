using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Forester : IComponentData
    {
        public Building Building { get; set; }
        public Vector2Int WorkArea { get; set; }
        public PlantTreeJob? Job { get; set; }
    }
}
