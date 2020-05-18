using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Carrier : IComponentData
    {
        public Vector2Int Pos { get; set; }
        public CarryInJob? Job { get; set; }
    }
}
