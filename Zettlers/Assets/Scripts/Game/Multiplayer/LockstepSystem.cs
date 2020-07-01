using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zettlers
{
    abstract class LockstepSystem : FixedUpdateSystem
    {
        private const int PlannedLatencyInLockstepTurns = 10;
        protected int LockstepTurnId = 0 - PlannedLatencyInLockstepTurns;
        private Queue<PlayerCommand> CommandsBeingSent = new Queue<PlayerCommand>();

        protected override void OnFixedUpdate()
        {
            if (NetworkingCommonConstants.IsServer)
            {
                if (
                    World.GetOrCreateSystem<ServerLockstepSystem>().ReceivedConfirmation(PlannedLatencyInLockstepTurns)
                 || TurnId < PlannedLatencyInLockstepTurns)
                {
                    OnLockstepUpdateInner();
                }
            }
            else
            {
                if (
                    World.GetOrCreateSystem<ClientLockstepSystem>().ReceivedConfirmation(PlannedLatencyInLockstepTurns)
                     || TurnId < PlannedLatencyInLockstepTurns)
                {
                    OnLockstepUpdateInner();
                }
            }

            TurnId++;
            LockstepTurnId = TurnId - PlannedLatencyInLockstepTurns;
        }

        private void OnLockstepUpdateInner()
        {
            Debug.Log("GameFrame:" + TurnId + " " + this.GetType().Name);
            OnLockstepUpdate();
        }

        protected abstract void OnLockstepUpdate();
    }    
}
