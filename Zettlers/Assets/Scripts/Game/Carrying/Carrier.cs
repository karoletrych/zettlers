using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public struct Carrier : IComponentData
    {
        public ResourceType? CarriedResource;
        public CarriageJob? Job;
    }
}
