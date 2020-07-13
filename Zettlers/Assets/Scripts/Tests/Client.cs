using UnityEngine;
using System;
using Unity.Entities;

namespace zettlers.Test
{
    public class Client : MonoBehaviour
    {
        private zettlers.Client _client;

        void Start()
        {
            World.DefaultGameObjectInjectionWorld.DestroyAllSystemsAndLogException();

            _client = new zettlers.Client();
        }

        public void Update() {

            LockstepUpdateRequest command = new LockstepUpdateRequest
            {
                LockstepTurnId = 2,
                PlayerCommand = new BuildBuildingCommand
                {
                    BuildingId = Guid.NewGuid(),
                    BuildingType = BuildingType.ForesterHut,
                    Position = new Unity.Mathematics.int2(1, 2)
                }
            };
            _client.Send(command);

            _client.PollResponses();
        }

        void OnDestroy()
        {
            _client.OnDestroy();
        }

    }
}