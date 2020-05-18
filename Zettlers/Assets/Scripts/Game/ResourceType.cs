using System.Collections.Generic;

namespace zettlers
{
    public static class ResourceTypeUtils
    {
        public static IEnumerable<ResourceType> AllResources => new[] {
            ResourceType.Wood,
            ResourceType.Planks,
            ResourceType.Stone,
            ResourceType.Gold
        };
    }

    public enum ResourceType
    {
        Wood,
        Planks,
        Stone,
        Gold
    }
}
