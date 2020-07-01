using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{
    [UpdateAfter(typeof(InputSystem))]
    class PlayerCommandProcessorSystem : LockstepSystem
    {
        public NativeQueue<CarriageJob> CarriageJobQueue;

        protected override void OnCreate()
        {
            CarriageJobQueue = new NativeQueue<CarriageJob>(Allocator.Persistent);
        }

        protected override void OnDestroy()
        {
            CarriageJobQueue.Dispose();
        }

        protected override void OnLockstepUpdate()
        {
            Debug.Log("[PlayerCommandProcessorSystem] Processing player commands");

            Debug.Log(LockstepTurnId);
            if (LockstepTurnId < 0)
            {
                return;
            }

            Dictionary<Player, LockstepUpdateRequest> playerUpdates = null;
            if (NetworkingCommonConstants.IsServer)
            {
                playerUpdates =
                    World.GetExistingSystem<ServerLockstepSystem>()
                    .LockstepUpdateBuffer.DequeueUpdatesForTurn(LockstepTurnId);
            }
            else
            {
                playerUpdates =
                    World.GetExistingSystem<ClientLockstepSystem>()
                    .LockstepUpdateBuffer.DequeueUpdatesForTurn(LockstepTurnId);
            }

            foreach (var update in playerUpdates)
            {
                PlayerCommand command = update.Value.PlayerCommand;
                Process(command);
            }
        }

        private void Process(PlayerCommand command)
        {
            if (command is NoCommand)
                return;
            Debug.Log("[PlayerCommandProcessorSystem] Player command" + command);

            BuildBuildingCommand buildCommand = (BuildBuildingCommand)command;
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Entity buildingEntity = entityManager.Instantiate(BuildingSpaceConverter.BuildingSpaceEntity);

            Building buildingData = new Building
            {
                Id = buildCommand.BuildingId,
                Type = buildCommand.BuildingType,
                Position = buildCommand.Position,
                Entity = buildingEntity
            };

            entityManager.AddComponentData(buildingEntity, buildingData);
            entityManager.AddComponentData(buildingEntity, new GameWorldPosition { Position = buildCommand.Position });

            int woodRequired = BuildingTypeUtils.ResourcesRequired(buildCommand.BuildingType)[ResourceType.Wood];
            entityManager.AddComponentData(buildingEntity, new ConstructionSite { WoodStillRequired = woodRequired });

            float3 position = buildCommand.Position.ToFloat3();
            entityManager.SetComponentData(buildingEntity,
                new Translation { Value = position });

            foreach (var resource in buildCommand.BuildingType.ResourcesRequired())
            {
                for (int i = 0; i < resource.Value; i++)
                {
                    CarriageJob jobData = new CarriageJob
                    {
                        ResourceType = resource.Key,
                        TargetBuilding = buildingData
                    };
                    CarriageJobQueue.Enqueue(jobData);
                }
            }
        }
    }
}
