using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zettlers
{
    class CarrierJobAssignerSystem : ISystem
    {
        public CarrierJobAssignerSystem(
            ResourcePriorityList priorityList, 
            CarrierJobQueue jobQueue,
            ZettlersList zettlersList)
        {
            _priorityList = priorityList;
            _jobQueue = jobQueue;
            _zettlersList = zettlersList;
        }
        private readonly ResourcePriorityList _priorityList;
        private readonly CarrierJobQueue _jobQueue;
        private readonly ZettlersList _zettlersList;

        public void Process()
        {
            List<Carrier> freeCarriers = _zettlersList.GetZettlers<Carrier>().Where(c => c.Job == null).ToList();

            foreach (var resourceType in _priorityList.PriorityList)
            {
                if (_jobQueue.Queue[resourceType].Count == 0)
                    continue;

                double minDist = float.MaxValue;
                foreach (CarryInJob job in _jobQueue.Queue[resourceType])
                {
                    if (freeCarriers.Count == 0)
                        return;

                    Carrier minDistCarrier = freeCarriers[0];
                    foreach (Carrier carrier in freeCarriers)
                    {
                        double dist = Vector2.Distance(carrier.Pos, job.Target.Position);
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
