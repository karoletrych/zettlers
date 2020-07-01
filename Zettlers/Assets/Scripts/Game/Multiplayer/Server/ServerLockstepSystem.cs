using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    static class Players
    {
        public static Dictionary<int, Player> PlayerIdToPlayerMapping = new Dictionary<int, Player>();
    }

    [UpdateAfter(typeof(InputSystem))]
    class ServerLockstepSystem : FixedUpdateSystem
    {
        public LockstepUpdateBuffer LockstepUpdateBuffer;

        private IServer _server;
        protected override void OnCreate()
        {
            
            LockstepUpdateBuffer = new LockstepUpdateBuffer(NetworkingCommonConstants.Players);
        }

        private void SaveUpdate(object sender, RequestReceivedEventArgs e)
        {
            Player player = Players.PlayerIdToPlayerMapping[e.ClientId];

            LockstepUpdateBuffer.Add(player, e.Request);
        }

        public bool ReceivedConfirmation(int plannedLatencyInLockstepTurns)
        {
            var hasAllCommands = LockstepUpdateBuffer.HasAllRequestsForTurn(TurnId - plannedLatencyInLockstepTurns);
            return hasAllCommands;
        }

        protected override void OnFixedUpdate()
        {
            if (!NetworkingCommonConstants.IsServer)
            {
                return;
            }

            GameObject networking = GameObject.Find("Networking");
            _server = networking.GetComponent<Server>();
            _server.RequestReceivedEvent -= SaveUpdate;
            _server.RequestReceivedEvent += SaveUpdate;
            _server.PlayerConnected += (sender, playerId) => Players.PlayerIdToPlayerMapping.Add(playerId, NetworkingCommonConstants.Players[0]);

            _server.PollEvents();
        }

        protected override void OnDestroy()
        {
        }
    }
}