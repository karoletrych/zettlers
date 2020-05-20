using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Carrier : IComponentData
    {
        public CarryInJob? Job;
    }
}
