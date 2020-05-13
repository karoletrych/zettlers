using System.Collections.Generic;

namespace zettlers
{
    class ResourcePriorityList
    {
        public IReadOnlyList<ResourceType> PriorityList { get; } = new List<ResourceType>(){
            ResourceType.Stone,
            ResourceType.Planks,
            ResourceType.Wood,
            ResourceType.Gold
        };
    }
}
