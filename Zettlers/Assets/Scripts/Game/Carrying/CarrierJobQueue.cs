using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace zettlers
{
    class CarrierJobQueue
    {
        public CarrierJobQueue(ResourcePriorityList priorityList)
        {
            foreach (var resourceType in ResourceType.Values)
            {
                _queue.Add(resourceType, new List<CarryInJob>());
            }
        }

        public IReadOnlyDictionary<ResourceType, List<CarryInJob>> Queue =>
            new ReadOnlyDictionary<ResourceType, List<CarryInJob>>(_queue);
        private readonly Dictionary<ResourceType, List<CarryInJob>> _queue = new Dictionary<ResourceType, List<CarryInJob>>();

        public void Enqueue(CarryInJob job)
        {
            _queue[job.ResourceType].Add(job);
        }
    }
}
