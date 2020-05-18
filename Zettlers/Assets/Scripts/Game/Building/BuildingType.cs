using System.Collections.Generic;

namespace zettlers
{
    public class BuildingType
    {
        public IReadOnlyDictionary<ResourceType, int> ResourcesRequired { get; set; }
        public string Name { get; set; }

        public static readonly BuildingType WoodcuttersHut = new BuildingType
        {
            Name = "Woodcutters Hut",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType ForesterHut = new BuildingType
        {
            Name = "Forester Hut",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType LumberMill = new BuildingType
        {
            Name = "Lumber Mill",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
        public static readonly BuildingType StonecuttersHut = new BuildingType
        {
            Name = "Stonecutters Hut",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };

        public static readonly BuildingType MediumResidence = new BuildingType
        {
            Name = "Medium Residence",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };

        public static readonly BuildingType StorageArea = new BuildingType
        {
            Name = "Storage Area",
            ResourcesRequired = new Dictionary<ResourceType, int> {
                {ResourceType.Planks, 2},
                {ResourceType.Stone, 1}
            }
        };
    }
}
