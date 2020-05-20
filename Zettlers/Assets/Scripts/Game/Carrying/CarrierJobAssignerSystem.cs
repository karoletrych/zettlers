using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace zettlers
{
    /// <summary>
    /// FIXME:
    /// This system requires to choose specific entities from 3 groups (Jobs, Carriers, Resources) 
    /// and write to each of them.
    /// Perhaps it shouldn't be done every frame.
    /// </summary>
    class CarrierJobAssignerSystem : ComponentSystem
    {
        private EntityQuery _carriersQuery;
        private EntityQuery _woodQuery;

        protected override void OnCreate()
        {
            _carriersQuery = GetEntityQuery(typeof(Carrier), typeof(GameWorldPosition));
            _woodQuery = GetEntityQuery(typeof(Resource), typeof(GameWorldPosition));
        }
        protected override void OnUpdate()
        {
            NativeArray<Entity> carriers =
                _carriersQuery.ToEntityArray(Allocator.Temp);
            NativeArray<Entity> allResources =
                _woodQuery.ToEntityArray(Allocator.Temp);

            NativeList<Entity> freeCarriers = new NativeList<Entity>(Allocator.Temp);
            for (int i = 0; i < carriers.Length; i++)
            {
                Carrier carrierData = GetComponentDataFromEntity<Carrier>(true)[carriers[i]];
                if (carrierData.Job == null)
                {
                    freeCarriers.Add(carriers[i]);
                }
            }

            foreach (ResourceType jobResourceType in ResourceCarryingPriorityList.PriorityList)
            {
                float minDist = float.MaxValue;
                while(CarrierJobQueue.Instance.Queues[jobResourceType].Count != 0)
                {
                    NativeList<Entity> jobResources = new NativeList<Entity>(Allocator.Temp);
                    for (int i = 0; i < allResources.Length; i++)
                    {
                        Resource resource = GetComponentDataFromEntity<Resource>(true)[allResources[i]];
                        if (resource.ResourceType == jobResourceType && !resource.Reserved)
                        {
                            jobResources.Add(allResources[i]);
                        }
                    }
                    if (jobResources.Length == 0)
                    {
                        goto nextResource;
                    }

                    CarryJob job = CarrierJobQueue.Instance.Queues[jobResourceType].Dequeue();

                    int minDistJobResourceIdx = -1;
                    Entity minDistResource = jobResources[0];
                    float minDistResourceToTarget = float.MaxValue;
                    GameWorldPosition minDistResourcePosition = new GameWorldPosition { };
                    for (int i = 0; i < jobResources.Length; i++)
                    {
                        GameWorldPosition resourcePosition = GetComponentDataFromEntity<GameWorldPosition>(true)[jobResources[i]];
                        Entity resource = jobResources[i];

                        float resourceToTargetDistance = Vector2.Distance(
                            resourcePosition.Position,
                            job.TargetBuildingPosition
                            );

                        if (resourceToTargetDistance < minDistResourceToTarget)
                        {
                            minDistJobResourceIdx = i;
                            minDist = resourceToTargetDistance;
                            minDistResource = resource;
                            minDistResourcePosition = resourcePosition;
                        }
                    }

                    if (freeCarriers.Length == 0)
                    {
                        freeCarriers.Dispose();
                        return;
                    }

                    int minDistCarrierIdx = -1;
                    Entity minDistCarrier = freeCarriers[0];
                    float minDistCarrierToResource = float.MaxValue;
                    for (int i = 0; i < freeCarriers.Length; i++)
                    {
                        Entity carrier = freeCarriers[i];
                        GameWorldPosition carrierPositionData =
                            GetComponentDataFromEntity<GameWorldPosition>(true)[carrier];

                        float carrierToResourceDistance = Vector2.Distance(
                            carrierPositionData.Position,
                            minDistResourcePosition.Position
                            );

                        if (carrierToResourceDistance < minDistCarrierToResource)
                        {
                            minDistCarrierIdx = i;
                            minDistCarrierToResource = carrierToResourceDistance;
                            minDistCarrier = carrier;
                        }
                    }
                    Debug.Log("Assigned");
                    CarrierJobQueue.Instance.Queues[jobResourceType].Dequeue();
                    EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                    entityManager.SetComponentData(minDistCarrier, new Carrier { Job = job });
                    entityManager.SetComponentData(minDistResource, new Resource { Reserved = true });

                    freeCarriers.RemoveAtSwapBack(minDistCarrierIdx);
                }

                nextResource:;
            }

            freeCarriers.Dispose();
        }
    }
}
