using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Builder : IComponentData
    {
        public Vector2Int Position { get; set; }
        public BuildJob? Job { get; set; }
    }
}
