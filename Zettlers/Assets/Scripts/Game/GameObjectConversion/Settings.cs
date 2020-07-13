using Unity.Entities;

namespace zettlers
{
    [GenerateAuthoringComponent]
    public struct Settings : IComponentData
    {
        public bool IsServer;
    }

}
