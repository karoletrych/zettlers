using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    struct Carrier : IComponentData
    {
        public bool CarriesResource;
        public CarryJob? Job;
    }
}
