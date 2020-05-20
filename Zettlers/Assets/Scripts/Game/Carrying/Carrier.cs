using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Carrier : IComponentData
    {
        public ResourceType? CarriedResource;
        public CarryJob? Job;
    }
}
