using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{

    [UpdateAfter(typeof(InputSystem))]
    public class ClientLockstepSystem : FixedUpdateSystem
    {
        private static PlayerCommand NoCommand = new NoCommand();

        public LockstepUpdateBuffer LockstepUpdateBuffer;
        private IClient _client;

        protected override void OnCreate()
        {
            LockstepUpdateBuffer = new LockstepUpdateBuffer(NetworkingCommonConstants.Players);

            if (GetSingleton<Settings>().IsServer)
            {
                return;
            }
            _client = new Client();
            _client.ResponseReceivedEvent += ReceiveResponse;

        }

        public bool ReceivedConfirmation(int plannedLatencyInLockstepTurns)
        {
            var hasAllCommands = LockstepUpdateBuffer.HasAllRequestsForTurn(TurnId - plannedLatencyInLockstepTurns);
            return hasAllCommands;
        }

        protected override void OnFixedUpdate()
        {



            Debug.Log("[SendPlayerCommandSystem] Receiving responses");
            _client.PollResponses();

            Debug.Log("[SendPlayerCommandSystem] Sending request");
            PlayerCommand command = World.GetOrCreateSystem<InputSystem>().Command;
            if (command == null)
            {
                SendCommand(NoCommand);
            }
            else
            {
                SendCommand(command);
            }
        }

        private void ReceiveResponse(object sender, LockstepUpdateResponse response)
        {
            foreach (var command in response.PlayerCommands)
            {
                if(command.Key == NetworkingCommonConstants.ThisPlayer)
                    continue;
                LockstepUpdateBuffer.Add(command.Key, 
                    new LockstepUpdateRequest {
                        LockstepTurnId = response.LockstepTurnId,
                        PlayerCommand = command.Value
                    });
            }
        }

        private void SendCommand(PlayerCommand playerCommand)
        {
            Debug.Log("[SendPlayerCommandSystem] Player command" + playerCommand);

            LockstepUpdateRequest update = new LockstepUpdateRequest
            {
                LockstepTurnId = TurnId,
                PlayerCommand = playerCommand
            };

            LockstepUpdateBuffer.Add(NetworkingCommonConstants.ThisPlayer, update);
            _client.Send(update);
        }

    }
}
