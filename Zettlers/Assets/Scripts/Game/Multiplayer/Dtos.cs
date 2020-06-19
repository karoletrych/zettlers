using System.Collections.Generic;

namespace zettlers
{

    public class LockstepUpdate
    {
        public IPlayerCommand PlayerCommand { get; set; }
        public int LockstepTurnId { get; set; }
    }

    public class Request
    {
        public LockstepUpdate LockstepUpdate { get; set; }
    }


    public class Response
    {
        public Dictionary<Player, LockstepUpdate> PlayerCommands { get; set; }
        public int LockstepTurnId { get; set; }
    }

    public class NoCommand : IPlayerCommand
    {
    }
}