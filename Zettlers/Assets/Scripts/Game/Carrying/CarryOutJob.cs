using Unity.Entities;

namespace zettlers
{
    struct CarryOutJob : IComponentData, IJob
    {
        public Building Source { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
