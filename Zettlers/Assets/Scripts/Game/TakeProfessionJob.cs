using Unity.Entities;

namespace zettlers
{
    struct TakeProfessionJob : IJob, IComponentData
    {
        public Building Building { get; set; }
    }
}
