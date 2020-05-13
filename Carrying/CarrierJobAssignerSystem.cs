using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    class CarrierJobAssignerSystem : ISystem
    {
        public CarrierJobAssignerSystem(ResourcePriorityList priorityList, CarrierJobQueue jobQueue, 
        ZettlersList zettlersList)
        {
            _priorityList = priorityList;
            _jobQueue = jobQueue;
            _zettlersList = zettlersList;
        }
        private readonly ResourcePriorityList _priorityList;
        private readonly CarrierJobQueue _jobQueue;
        private ZettlersList _zettlersList;

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
                        double dist = carrier.Pos.Dist(job.Target.Pos);
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
