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

            InputSystem inputSystem = World.GetOrCreateSystem<InputSystem>();

            IPlayerCommand command = inputSystem.Command;
            if(command == null)
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
