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
    /// This system requires to choose specific entities from 2 groups (Jobs and Carriers) 
    /// and write to each of them.
    /// Perhaps it shouldn't be done every frame.
    /// </summary>
    class CarrierJobAssignerSystem : ComponentSystem
    {
        private EntityQuery _carriersQuery;

        protected override void OnCreate()
        {
            _carriersQuery = GetEntityQuery(typeof(Carrier), typeof(GameWorldPosition));
        }
        protected override void OnUpdate()
        {
            NativeArray<Entity> carriers =
                _carriersQuery.ToEntityArray(Allocator.Temp);

            NativeList<Entity> freeCarriers = new NativeList<Entity>(Allocator.Temp);
            for (int i = 0; i < carriers.Length; i++)
            {
                Carrier carrierData = GetComponentDataFromEntity<Carrier>(true)[carriers[i]];
                if (carrierData.Job == null)
                {
                    freeCarriers.Add(carriers[i]);
                }
            }

            foreach (ResourceType resourceType in ResourceCarryingPriorityList.PriorityList)
            {
                if (CarrierJobQueue.Instance.Queue[resourceType].Count == 0)
                    continue;

                double minDist = float.MaxValue;
                foreach (CarryInJob job in CarrierJobQueue.Instance.Queue[resourceType])
                {
                    if (freeCarriers.Length == 0)
                    {
                        freeCarriers.Dispose();
                        return;
                    }

                    int minDistCarrierIdx = -1;
                    Entity minDistCarrier = freeCarriers[0];
                    for (int i = 0; i < freeCarriers.Length; i++)
                    {
                        Entity carrier = freeCarriers[i];
                        GameWorldPosition positionData = 
                            GetComponentDataFromEntity<GameWorldPosition>(true)[carrier];

                        float dist = Vector2.Distance(
                            positionData.Position, 
                            job.TargetBuildingPosition);

                        if (dist < minDist)
                        {
                            minDistCarrierIdx = i;
                            minDist = dist;
                            minDistCarrier = carrier;
                        }
                    }

                    EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                    entityManager.SetComponentData(minDistCarrier, new Carrier {Job = job});
                    freeCarriers.RemoveAtSwapBack(minDistCarrierIdx);

                }

                freeCarriers.Dispose();
            }
        }
    }
}
