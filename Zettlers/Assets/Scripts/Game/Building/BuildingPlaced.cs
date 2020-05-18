using UnityEngine;

namespace zettlers
{
    class BuildingPlaced : IPlayerCommand
    {
        public BuildingType BuildingType { get; set; }
        public Vector2Int Position { get; set; }
    }
}
