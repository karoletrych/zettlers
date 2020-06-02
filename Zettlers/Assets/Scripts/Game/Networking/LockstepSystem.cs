using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zettlers
{
    abstract class LockstepSystem : FixedUpdateSystem
    {
        private const int PlannedLatencyInLockstepTurns = 20;
        private Queue<IPlayerCommand> CommandsBeingSent = new Queue<IPlayerCommand>();

        protected override void OnFixedUpdate()
        {
            if (!Networking.IsServer)
            {
                ClientLockstepUpdate();
            }
            else
            {
                ServerLockstepUpdate();
            }

            TurnId++;
        }

        private void ClientLockstepUpdate()
        {
            if (ReceivedConfirmation() || TurnId < PlannedLatencyInLockstepTurns)
            {
                OnLockstepUpdate();
            }
        }

        private void ServerLockstepUpdate()
        {
        }

        private bool ReceivedConfirmation()
        {
            var sender = World.GetOrCreateSystem<SendPlayerCommandSystem>();
            PlayerCommandRequest firstCommand = sender.CommandsToSendQueue.FirstOrDefault();
            bool receivedConfirmation = 
                firstCommand == null || 
                firstCommand.LockstepTurnId >= TurnId - PlannedLatencyInLockstepTurns;

            return receivedConfirmation;
        }

        protected abstract void OnLockstepUpdate();
    }    
}
