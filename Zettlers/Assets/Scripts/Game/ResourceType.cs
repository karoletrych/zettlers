using System.Collections.Generic;

namespace zettlers
{
    public class ResourceType
    {
        public static IEnumerable<ResourceType> Values => new[] {
            ResourceType.Wood,
            ResourceType.Planks,
            ResourceType.Stone,
            ResourceType.Gold
        };
        public static readonly ResourceType Wood = new ResourceType();
        public static readonly ResourceType Planks = new ResourceType();
        public static readonly ResourceType Stone = new ResourceType();
        public static readonly ResourceType Gold = new ResourceType();
    }
}
