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
            Entity buildingEntity = entityManager.CreateEntity(typeof(Building));
            entityManager.SetComponentData(buildingEntity, buildingData);

            GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            GameObject buildingSpace = Resources.Dict["BuildingSpace"];

            Debug.Log(buildingSpace);

            Entity buildingSpacePrototype = GameObjectConversionUtility.ConvertGameObjectHierarchy(buildingSpace, settings);

            Entity instancee = entityManager.Instantiate(buildingSpacePrototype);
            var position = GameObject.Find("ECS")
                .transform.TransformPoint(new Vector3(command.Position.x * 1.3F, 7, command.Position.y * 1.3F));
            entityManager.SetComponentData(instancee, new Translation {Value = position});

            Debug.Log(position);
            entityManager.SetComponentData(buildingSpacePrototype, new Translation {Value = position});
            
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
