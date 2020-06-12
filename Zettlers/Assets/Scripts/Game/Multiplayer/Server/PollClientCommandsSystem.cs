using Unity.Entities;
using UnityEngine;

namespace zettlers
{

    [UpdateAfter(typeof(InputSystem))]
    class PollClientCommandsSystem : FixedUpdateSystem
    {
        private static IPlayerCommand NoCommand = new NoCommand();

        private Server _server;
        protected override void OnCreate()
        {
            GameObject networking = GameObject.Find("Networking");
            _server = networking.GetComponent<Server>();
            _server.NetworkReceivedEvent += SaveRequest;
        }

        protected override void OnDestroy()
        {
            _server.NetworkReceivedEvent -= SaveRequest;
        }
        
        private void SaveRequest(object sender, PlayerRequest playerRequest)
        {
            // PlayerCommands[playerRequest.PlayerId].Enqueue(playerRequest.Request);
        }

        protected override void OnFixedUpdate()
        {
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

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
        }

        protected override void OnStopRunning()
        {
            base.OnStopRunning();
        }

        private void SendCommand(IPlayerCommand playerCommand)
        {
            Debug.Log("[PollClientCommandsSystem] Player command" + playerCommand);

            // server.Pol;
        }
    }
}
