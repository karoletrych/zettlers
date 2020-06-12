using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{

    [UpdateAfter(typeof(InputSystem))]
    class SendPlayerCommandSystem : FixedUpdateSystem
    {
        private static IPlayerCommand NoCommand = new NoCommand();

        public List<Request> CommandsToSendQueue = new List<Request>();

        protected override void OnFixedUpdate()
        {
            Debug.Log("[SendPlayerCommandSystem] Sending player command");

            InputSystem inputSystem = World.GetOrCreateSystem<InputSystem>();

            IPlayerCommand command = inputSystem.Command;
            if (command == null)
            {
                SendCommand(NoCommand);
            }
            else
            {
                SendCommand(command);
            }
        }

        private void SendCommand(IPlayerCommand playerCommand)
        {
            Debug.Log("[SendPlayerCommandSystem] Player command" + playerCommand);

            var clientObject = GameObject.Find("Networking");
            Client client = clientObject.GetComponent<Client>();

            InputSystem inputSystem = World.GetOrCreateSystem<InputSystem>();

            IPlayerCommand command = inputSystem.Command;
            if (command == null)
                return;

            BuildBuildingCommand buildCommand = (BuildBuildingCommand)command;

            Request request = new Request
            {
                LockstepTurnId = TurnId,
                PlayerCommand = command
            };
            CommandsToSendQueue.Add(request);
            client.Send(request);
        }
    }
}
