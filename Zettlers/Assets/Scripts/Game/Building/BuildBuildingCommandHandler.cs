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
                Id = command.Id,
                Type = command.BuildingType,
                Position = command.Position
            };

            Entity buildingEntity = entityManager.Instantiate(PrefabEntities.BuildingSpaceEntity);
            entityManager.AddComponentData(buildingEntity, buildingData);

            Vector3 position = GameObject.Find("ECS")
                .transform.TransformPoint(new Vector3(command.Position.x * 1.3F, 7, command.Position.y * 1.3F));
            entityManager.SetComponentData(buildingEntity, new Translation {Value = position});
            
            foreach (KeyValuePair<ResourceType, int> resource in 
                command.BuildingType.ResourcesRequired())
            {
                for (int i = 0; i < resource.Value; i++)
                {
                    CarryInJob jobData = new CarryInJob
                    {
                        ResourceType = resource.Key,
                        TargetBuildingId = buildingData.Id
                    };
                    CarrierJobQueue.Instance.Enqueue(jobData);
                }
            }
        }
    }
}
