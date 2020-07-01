using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace zettlers
{

    public class LockstepUpdateRequest
    {
        public int LockstepTurnId { get; set; }
        public PlayerCommand PlayerCommand { get; set; }
    }

    public abstract class PlayerCommand
    {
    }

    public class LockstepUpdateResponse
    {
        public Dictionary<Player, PlayerCommand> PlayerCommands { get; set; }
        public int LockstepTurnId { get; set; }
    }


    public class NoCommand : PlayerCommand
    {
    }

    public class BuildBuildingCommand : PlayerCommand
    {
        public Guid BuildingId { get; set; }

        public BuildingType BuildingType;
        public int2 Position;

    }

}
