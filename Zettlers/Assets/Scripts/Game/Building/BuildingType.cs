using System.Collections.Generic;

namespace zettlers
{
    public enum BuildingType
    {
        WoodcuttersHut,
        ForesterHut,
        LumberMill,
        StonecuttersHut,
        MediumResidence,
        StorageArea
    }

    public static class BuildingTypeUtils
    {
        public static string Name(this BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.WoodcuttersHut:
                    return "Woodcutters Hut";
                case BuildingType.ForesterHut:
                    return "Foresters Hut";
                case BuildingType.LumberMill:
                    return "Lumber Mill";
                case BuildingType.StonecuttersHut:
                    return "Stonecutters Hut";
                case BuildingType.MediumResidence:
                    return "Medium Residence";
                case BuildingType.StorageArea:
                    return "Storage Area";
                default:
                    throw new System.Exception();
            }
        }
        public static IReadOnlyDictionary<ResourceType, int> ResourcesRequired(this BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.WoodcuttersHut:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                case BuildingType.ForesterHut:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                case BuildingType.LumberMill:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                case BuildingType.StonecuttersHut:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                case BuildingType.MediumResidence:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                case BuildingType.StorageArea:
                    return new Dictionary<ResourceType, int> {
                        {ResourceType.Planks, 2},
                        {ResourceType.Stone, 1}
                    };
                default:
                    throw new System.Exception();
            }
        }
    }
}
