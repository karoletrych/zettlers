using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{
    class InputSystem : LockstepSystem
    {
        private BuildingType BuildingSelected = BuildingType.ForesterHut;

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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BuildingSelected = BuildingType.WoodcuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BuildingSelected = BuildingType.ForesterHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BuildingSelected = BuildingType.StonecuttersHut;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                BuildingSelected = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                BuildingSelected = BuildingType.MediumResidence;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                BuildingSelected = BuildingType.StorageArea;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                GameObject ground = GameObject.Find("Ground_01");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (ground.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log(
                        "Building type:" + BuildingSelected.Name() +
                        " position:" + hit.point);
                    BuildBuilding(new BuildBuildingCommand
                    {
                        BuildingType = BuildingSelected,
                        BuildingId = Guid.NewGuid(),
                        Position = hit.point.ToInt2()
                    });
                }
            }
        }

        private void BuildBuilding(BuildBuildingCommand command)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity buildingEntity = entityManager.Instantiate(BuildingSpaceConverter.BuildingSpaceEntity);

            Building buildingData = new Building
            {
                Id = command.BuildingId,
                Type = command.BuildingType,
                Position = command.Position,
                Entity = buildingEntity
            };

            entityManager.AddComponentData(buildingEntity, buildingData);
            entityManager.AddComponentData(buildingEntity, new GameWorldPosition { Position = command.Position });

            int woodRequired = BuildingTypeUtils.ResourcesRequired(command.BuildingType)[ResourceType.Wood];
            entityManager.AddComponentData(buildingEntity, new ConstructionSite { WoodStillRequired = woodRequired });

            float3 position = command.Position.ToFloat3();
            entityManager.SetComponentData(buildingEntity,
                new Translation { Value = position });

            foreach (KeyValuePair<ResourceType, int> resource in
                command.BuildingType.ResourcesRequired())
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
