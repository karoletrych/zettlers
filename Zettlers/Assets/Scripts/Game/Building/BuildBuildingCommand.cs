using System;
using LiteNetLib.Utils;
using Unity.Mathematics;
using UnityEngine;

namespace zettlers
{
    [Serializable]
    public struct BuildBuildingCommand : IPlayerCommand
    {
        public Guid BuildingId;
        public BuildingType BuildingType;
        public int2 Position;
    }
}
