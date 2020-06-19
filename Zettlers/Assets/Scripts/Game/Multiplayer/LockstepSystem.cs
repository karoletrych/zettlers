using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zettlers
{
    abstract class LockstepSystem : FixedUpdateSystem
    {
        private const int PlannedLatencyInLockstepTurns = 200;
        protected int LockstepTurnId = 0 - PlannedLatencyInLockstepTurns;
        private Queue<IPlayerCommand> CommandsBeingSent = new Queue<IPlayerCommand>();

        protected override void OnFixedUpdate()
        {
            if (NetworkingCommonConstants.IsServer)
            {
                if (
                    World.GetOrCreateSystem<ServerLockstepSystem>().ReceivedConfirmation(PlannedLatencyInLockstepTurns)
                 || TurnId < PlannedLatencyInLockstepTurns)
                {
                    OnLockstepUpdate();
                }
            }
            else
            {
                if (
                    World.GetOrCreateSystem<ClientLockstepSystem>().ReceivedConfirmation(PlannedLatencyInLockstepTurns)
                     || TurnId < PlannedLatencyInLockstepTurns)
                {
                    OnLockstepUpdate();
                }
            }

            TurnId++;
            LockstepTurnId = TurnId - PlannedLatencyInLockstepTurns;
        }

        protected abstract void OnLockstepUpdate();
    }    
}
