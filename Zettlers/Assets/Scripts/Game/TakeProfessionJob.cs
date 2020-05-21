using Unity.Entities;

namespace zettlers
{
    struct TakeProfessionJob : IComponentData
    {
        public Building Building { get; set; }
    }
}
