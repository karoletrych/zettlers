using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    public class LockstepUpdateBuffer
    {
        private Dictionary<Player, List<LockstepUpdateRequest>> PlayerUpdates = new Dictionary<Player, List<LockstepUpdateRequest>>();

        public LockstepUpdateBuffer(List<Player> players)
        {
            foreach (var player in players)
            {
                PlayerUpdates[player] = new List<LockstepUpdateRequest>();
            } 
        }

        public void Add(Player player, LockstepUpdateRequest update)
        {
            PlayerUpdates[player].Add(update);
        }

        private Dictionary<Player, LockstepUpdateRequest> TryGetRequestsForTurn(int lockstepTurnId)
        {
            Dictionary<Player, LockstepUpdateRequest> requests = new Dictionary<Player, LockstepUpdateRequest>();

            foreach (Player p in PlayerUpdates.Keys)
            {
                LockstepUpdateRequest update = PlayerUpdates[p].FirstOrDefault(u => u.LockstepTurnId == lockstepTurnId);
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

        public Dictionary<Player, LockstepUpdateRequest> DequeueUpdatesForTurn(int turn)
        {
            Dictionary<Player, LockstepUpdateRequest> requests = TryGetRequestsForTurn(turn);
            foreach (var player_lockstepUpdate in requests)
            {
                PlayerUpdates[player_lockstepUpdate.Key].RemoveAll(x => x.LockstepTurnId == turn);
            }

            return requests;
        }
    }
}
