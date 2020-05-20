using System;
using UnityEngine;

namespace zettlers
{
    class BuildBuildingCommand : IPlayerCommand
    {
        public Guid BuildingId { get; set; }
        public BuildingType BuildingType { get; set; }
        public Vector2Int Position { get; set; }
    }
}
