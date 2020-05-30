using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public abstract class FixedUpdateSystem : SystemBase
    {
        private readonly static TimeSpan FixedDeltaTime = TimeSpan.FromMilliseconds(10000);

        private DateTime _lastOnUpdateTimeStamp;

        protected override void OnCreate()
        {
            _lastOnUpdateTimeStamp = DateTime.Now;
        }
        protected override sealed void OnUpdate()
        {
            TimeSpan timeSinceLastUpdate = DateTime.Now - _lastOnUpdateTimeStamp;
            if (timeSinceLastUpdate >= FixedDeltaTime)
            {
                _lastOnUpdateTimeStamp = DateTime.Now;
                InternalOnFixedUpdate();
            }
        }

        private void InternalOnFixedUpdate() 
        {
            Debug.Log("elapsed" + Time.ElapsedTime * 10);
            OnFixedUpdate();
        }

        protected abstract void OnFixedUpdate();
    }


    public abstract class LockstepSystem : FixedUpdateSystem
    {
        private const int PlannedLatencyInLockstepTurns = 2;
        private long _lockStepTurnId = 5;

        // client
        protected override void OnFixedUpdate()
        {
            if (_lockStepTurnId > PlannedLatencyInLockstepTurns && ReceivedConfirmation())
            {
                OnLockstepUpdate();
            }
        }

        private bool ReceivedConfirmation()
        {
            return true;
        }

        // protected override void OnUpdate()
        // {
        //     if (((int)Time.ElapsedTime) % 2 == 0)
        //     {
        //         OnLockstepUpdate();
        //     }
        // }

        protected abstract void OnLockstepUpdate();
    }    
}
