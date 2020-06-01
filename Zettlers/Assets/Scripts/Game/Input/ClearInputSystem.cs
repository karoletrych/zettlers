using System;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{

    // clears input command so that its only processed once via playercommandprocessor and requestsender
    [UpdateAfter(typeof(PlayerCommandProcessorSystem))]
    [UpdateAfter(typeof(SendPlayerCommandSystem))]
    class ClearInputSystem : FixedUpdateSystem
    {
        protected override void OnFixedUpdate()
        {
            InputSystem inputSystem = World.GetOrCreateSystem<InputSystem>();
            if (inputSystem.Command != null)
            {
                Debug.Log("clearing commmand");
            }
            inputSystem.Command = null;
        }
    }
}
