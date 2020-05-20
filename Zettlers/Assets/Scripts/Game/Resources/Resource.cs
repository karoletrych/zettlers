using Unity.Entities;

namespace zettlers
{
    public struct Resource : IComponentData
    {
        public ResourceType ResourceType;
        public bool Reserved;
    }
}
