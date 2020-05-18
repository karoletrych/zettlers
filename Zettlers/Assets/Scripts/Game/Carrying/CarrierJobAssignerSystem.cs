using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace zettlers
{
    class CarrierJobAssignerSystem : ComponentSystem
    {
        private EntityQuery _carriersQuery;

        protected override void OnCreate()
        {
            _carriersQuery = GetEntityQuery(typeof(Carrier));
        }
        protected override void OnUpdate()
        {
            CarrierJobQueue jobQueue = CarrierJobQueue.Instance;

            NativeArray<Carrier> carriers = 
                _carriersQuery.ToComponentDataArray<Carrier>(Allocator.Temp);

            List<Carrier> freeCarriers = carriers
                .Where(c => c.Job == null).ToList();

            foreach (ResourceType resourceType in ResourcePriorityList.PriorityList)
            {
                if (jobQueue.Queue[resourceType].Count == 0)
                    continue;

                double minDist = float.MaxValue;
                foreach (CarryInJob job in jobQueue.Queue[resourceType])
                {
                    if (freeCarriers.Count == 0)
                        return;

                    Carrier minDistCarrier = freeCarriers[0];
                    foreach (Carrier carrier in freeCarriers)
                    {
                        float dist = Vector2.Distance(
                            carrier.Pos, 
                            job.TargetBuildingPosition);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            minDistCarrier = carrier;
                        }
                    }

                    minDistCarrier.Job = job;
                    freeCarriers.Remove(minDistCarrier);
                }
            }
        }
    }
}
