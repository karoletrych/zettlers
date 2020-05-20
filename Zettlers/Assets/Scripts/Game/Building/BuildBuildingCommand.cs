using System;
using UnityEngine;

namespace zettlers
{
    public class BuildBuildingCommand : IPlayerCommand
    {
        public Guid BuildingId { get; set; }
        public BuildingType BuildingType { get; set; }
        public Vector2Int Position { get; set; }
    }
}
