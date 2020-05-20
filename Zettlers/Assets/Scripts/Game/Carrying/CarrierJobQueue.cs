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
                _queue.Add(resourceType, new Queue<CarryJob>());
            }
        }

        public IReadOnlyDictionary<ResourceType, Queue<CarryJob>> Queues =>
            new ReadOnlyDictionary<ResourceType, Queue<CarryJob>>(_queue);
        private readonly Dictionary<ResourceType, Queue<CarryJob>> _queue = new Dictionary<ResourceType, Queue<CarryJob>>();

        public void Enqueue(CarryJob job)
        {
            _queue[job.ResourceType].Enqueue(job);
        }
    }
}
