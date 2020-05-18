using UnityEngine;

namespace zettlers
{
    class BuildBuildingCommand : IPlayerCommand
    {
        public BuildingType BuildingType { get; set; }
        public Vector2Int Position { get; set; }
    }
}
