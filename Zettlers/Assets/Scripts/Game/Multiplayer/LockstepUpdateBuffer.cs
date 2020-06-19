using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    public class LockstepUpdateBuffer
    {
        private Dictionary<Player, List<LockstepUpdate>> PlayerUpdates = new Dictionary<Player, List<LockstepUpdate>>();

        public LockstepUpdateBuffer(List<Player> players)
        {
            foreach (var player in players)
            {
                PlayerUpdates[player] = new List<LockstepUpdate>();
            } 
        }

        public void Add(Player player, LockstepUpdate update)
        {
            PlayerUpdates[player].Add(update);
        }

        private Dictionary<Player, LockstepUpdate> TryGetRequestsForTurn(int lockstepTurnId)
        {
            Dictionary<Player, LockstepUpdate> requests = new Dictionary<Player, LockstepUpdate>();

            foreach (Player p in PlayerUpdates.Keys)
            {
                LockstepUpdate update = PlayerUpdates[p].FirstOrDefault(u => u.LockstepTurnId == lockstepTurnId);
                if (update is null)
                    return null;
                else
                    requests[p] = update;
            }

            return requests;
        }

        public bool HasAllRequestsForTurn(int turn)
        {
            return TryGetRequestsForTurn(turn) != null;
        }

        public Dictionary<Player, LockstepUpdate> DequeueUpdatesForTurn(int turn)
        {
            Dictionary<Player, LockstepUpdate> requests = TryGetRequestsForTurn(turn);
            foreach (var player_lockstepUpdate in requests)
            {
                PlayerUpdates[player_lockstepUpdate.Key].RemoveAll(x => x.LockstepTurnId == turn);
            }

            return requests;
        }
    }
}
