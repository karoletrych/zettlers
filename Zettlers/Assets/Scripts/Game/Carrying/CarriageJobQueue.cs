using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace zettlers
{
    class CarrierJobQueue
    {
        public static CarrierJobQueue Instance = new CarrierJobQueue();

        public CarrierJobQueue()
        {
            foreach (var resourceType in ResourceTypeUtils.AllResources)
            {
                _queue.Add(resourceType, new Queue<CarriageJob>());
            }
        }

        public IReadOnlyDictionary<ResourceType, Queue<CarriageJob>> Queues =>
            new ReadOnlyDictionary<ResourceType, Queue<CarriageJob>>(_queue);
        private readonly Dictionary<ResourceType, Queue<CarriageJob>> _queue = new Dictionary<ResourceType, Queue<CarriageJob>>();

        public void Enqueue(CarriageJob job)
        {
            _queue[job.ResourceType].Enqueue(job);
        }
    }
}
