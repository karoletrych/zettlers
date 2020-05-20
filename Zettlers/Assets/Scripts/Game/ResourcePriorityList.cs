using System.Collections.Generic;

namespace zettlers
{
    static class ResourceCarryingPriorityList
    {
        public static IReadOnlyList<ResourceType> PriorityList { get; } = new List<ResourceType>(){
            ResourceType.Stone,
            ResourceType.Planks,
            ResourceType.Wood,
            ResourceType.Gold
        };
    }
}
