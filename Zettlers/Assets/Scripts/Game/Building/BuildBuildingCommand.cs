using System;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    public class BuildBuildingCommand : IPlayerCommand
    {
        public Guid BuildingId { get; set; }
        public BuildingType BuildingType { get; set; }
        public int2 Position { get; set; }
    }
}
