using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    public abstract class FixedUpdateSystem : SystemBase
    {
        protected int TurnId = 0;
        private readonly static TimeSpan FixedDeltaTime = TimeSpan.FromMilliseconds(1000);

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
            OnFixedUpdate();
        }

        protected abstract void OnFixedUpdate();
    }
}
