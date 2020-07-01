using System;
using System.Linq;
using Unity.Mathematics;
using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace zettlers
{
    public static class int2Serializer
    {
        public static int2 Deserialize(this Commands.int2 b)
        {
            return new int2 { x = b.X, y = b.Y };
        }
        public static Commands.int2 Serialize(this int2 b)
        {
            return new Commands.int2 { X = b.x, Y = b.y };
        }

    }
    public static class BuildBuildingCommandSerializer
    {
        public static BuildBuildingCommand Deserialize(this Commands.BuildBuildingCommand b)
        {
            return new BuildBuildingCommand
            {
                BuildingId = new Guid(b.BuildingId),
                BuildingType = (BuildingType)b.BuildingType,
                Position = int2Serializer.Deserialize(b.Position)
            };
        }

        public static Commands.BuildBuildingCommand Serialize(this BuildBuildingCommand b)
        {
            return new Commands.BuildBuildingCommand
            {
                BuildingId = b.BuildingId.ToString(),
                BuildingType = (int)b.BuildingType,
                Position = int2Serializer.Serialize(b.Position)
            };
        }
    }

    public static class PlayerCommandSerializer
    {
        public static PlayerCommand Deserialize(this Commands.PlayerCommand b)
        {
            if (b.OCase == Commands.PlayerCommand.OOneofCase.Bbc)
            {
                return BuildBuildingCommandSerializer.Deserialize(b.Bbc);
            }
            else if (b.OCase == Commands.PlayerCommand.OOneofCase.Nc)
            {
                return new NoCommand
                {
                };
            }
            throw new ArgumentException();
        }

        public static Commands.PlayerCommand Serialize(this PlayerCommand b)
        {
            Commands.PlayerCommand pc = new Commands.PlayerCommand();
            if (b is BuildBuildingCommand bbc)
            {
                pc.Bbc = BuildBuildingCommandSerializer.Serialize(bbc);
            }
            else if (b is NoCommand nc)
            {
                pc.Nc = new Commands.NoCommand();
            }
            return pc;
        }
    }

    public static class LockstepUpdateRequestSerializer
    {
        public static LockstepUpdateRequest Deserialize(this Commands.LockstepUpdateRequest b)
        {
            return new LockstepUpdateRequest
            {
                LockstepTurnId = b.LockstepTurnId,
                PlayerCommand = b.PlayerCommand.Deserialize()
            };
        }

        public static Commands.LockstepUpdateRequest Serialize(this LockstepUpdateRequest b)
        {
            return new Commands.LockstepUpdateRequest
            {
                LockstepTurnId = b.LockstepTurnId,
                PlayerCommand = b.PlayerCommand.Serialize()
            };
        }
    }


    public static class LockstepUpdatResponseSerializer
    {
        public static LockstepUpdateResponse Deserialize(this Commands.LockstepUpdateResponse b)
        {
            return new LockstepUpdateResponse
            {
                LockstepTurnId = b.LockstepTurnId,
                PlayerCommands = b.PlayerCommands.ToDictionary(p => new Player(p.Key), p => p.Value.Deserialize())
            };
        }

        public static Commands.LockstepUpdateResponse Serialize(this LockstepUpdateResponse b)
        {
            var rsp = new Commands.LockstepUpdateResponse
            {
                LockstepTurnId = b.LockstepTurnId,
            };

            rsp.PlayerCommands.Add(b.PlayerCommands.ToDictionary(p => p.Key.Name, p => p.Value.Serialize()));
            return rsp;
        }
    }
}
