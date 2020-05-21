using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Object = UnityEngine.Object;

namespace zettlers
{
    class BuildBuildingCommandHandler : IPlayerCommandHandler<BuildBuildingCommand>
    {
        public void Handle(BuildBuildingCommand command)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Building buildingData = new Building
            {
                Id = command.BuildingId,
                Type = command.BuildingType,
            };

            Entity buildingEntity = entityManager.Instantiate(BuildingSpaceConverter.BuildingSpaceEntity);
            entityManager.AddComponentData(buildingEntity, buildingData);
            entityManager.AddComponentData(buildingEntity, new GameWorldPosition{Position = command.Position});

            Vector3 position = command.Position.ToVector3();
            entityManager.SetComponentData(buildingEntity, 
                new Translation {Value = position});
            
            foreach (KeyValuePair<ResourceType, int> resource in 
                command.BuildingType.ResourcesRequired())
            {
                for (int i = 0; i < resource.Value; i++)
                {
                    CarriageJob jobData = new CarriageJob
                    {
                        ResourceType = resource.Key,
                        TargetBuildingId = buildingData.Id,
                        TargetBuildingPosition = command.Position
                    };
                    CarrierJobQueue.Instance.Enqueue(jobData);
                }
            }
        }
    }
}
