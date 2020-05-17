using System.Collections.Generic;

namespace zettlers
{
    class BuildingType
    {
        public IReadOnlyDictionary<ResourceType, int> ResourcesRequiredToBuild { get; set; }
        public static readonly BuildingType WoodcuttersHut = new BuildingType
        {
            ResourcesRequiredToBuild = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType ForesterHut = new BuildingType
        {
            ResourcesRequiredToBuild = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType LumberMill = new BuildingType
        {
            ResourcesRequiredToBuild = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType StonecuttersHut = new BuildingType
        {
            ResourcesRequiredToBuild = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
    }
}
