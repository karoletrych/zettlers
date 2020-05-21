using Unity.Entities;

namespace zettlers
{
    public struct BuildJob : IComponentData
    {
        public Building Building;
        public ResourceType ResourceType;
    }
}
