using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{

    class NoCommand : IPlayerCommand
    {
    }

    class PlayerCommandRequest
    {
        public IPlayerCommand PlayerCommand { get; set; }
        public int LockstepTurnId { get; set; }
    }

    [UpdateAfter(typeof(InputSystem))]
    class SendPlayerCommandSystem : FixedUpdateSystem
    {
        private static IPlayerCommand NoCommand = new NoCommand();

        public List<PlayerCommandRequest> CommandsToSendQueue = new List<PlayerCommandRequest>();

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
            var client = clientObject.GetComponent<Client>();

            InputSystem inputSystem = World.GetOrCreateSystem<InputSystem>();

            IPlayerCommand command = inputSystem.Command;
            if (command == null)
                return;

            BuildBuildingCommand buildCommand = (BuildBuildingCommand)command;

            CommandsToSendQueue.Add(new PlayerCommandRequest
            {
                LockstepTurnId = TurnId,
                PlayerCommand = command
            });
            client.SendCommand(command);
        }
    }
}
