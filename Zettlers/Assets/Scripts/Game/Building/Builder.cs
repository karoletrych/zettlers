using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct Builder : IComponentData
    {
        public BuildJob? Job { get; set; }
        public float CurrentBuildTime { get; set; }
    }
}
