using System;
using System.Collections.Generic;
using Unity.Entities;

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
