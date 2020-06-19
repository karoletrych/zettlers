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
        private static IPlayerCommand NoCommand = new NoCommand();

        public LockstepUpdateBuffer LockstepUpdateBuffer;
        private IClient _client;

        protected override void OnCreate()
        {
            var clientObject = GameObject.Find("Networking");
            _client = clientObject.GetComponent<Client>();

            _client.ResponseReceivedEvent += ReceiveResponse;

            LockstepUpdateBuffer = new LockstepUpdateBuffer(NetworkingCommonConstants.Players);
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
            IPlayerCommand command = World.GetOrCreateSystem<InputSystem>().Command;
            if (command == null)
            {
                SendCommand(NoCommand);
            }
            else
            {
                SendCommand(command);
            }
        }

        private void ReceiveResponse(object sender, Response response)
        {
            foreach (var command in response.PlayerCommands)
            {
                if(command.Key == NetworkingCommonConstants.ThisPlayer)
                    continue;
                LockstepUpdateBuffer.Add(command.Key, command.Value);
            }
        }

        private void SendCommand(IPlayerCommand playerCommand)
        {
            Debug.Log("[SendPlayerCommandSystem] Player command" + playerCommand);

            LockstepUpdate update = new LockstepUpdate
            {
                LockstepTurnId = TurnId,
                PlayerCommand = playerCommand
            };

            LockstepUpdateBuffer.Add(NetworkingCommonConstants.ThisPlayer, update);
            _client.Send(update);
        }

    }
}
