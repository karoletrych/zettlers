using Unity.Entities;

namespace zettlers
{
    struct BuildJob : IJob, IComponentData
    {
        public Building Building { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
