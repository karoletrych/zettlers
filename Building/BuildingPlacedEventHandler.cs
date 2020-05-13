using System;
using System.Collections.Generic;

namespace zettlers
{
    class BuildingPlacedEventHandler : IGameEventHandler<BuildingPlaced>
    {
        private readonly CarrierJobQueue _jobQueue;

        public BuildingPlacedEventHandler(CarrierJobQueue jobQueue)
        {
            _jobQueue = jobQueue;
        }

        public void Handle(BuildingPlaced @event)
        {
            Building building = new Building
            {
                Type = @event.BuildingType,
                Id = Guid.Empty,
                Pos = @event.Pos
            };

            foreach (KeyValuePair<ResourceType, int> resource in 
                @event.BuildingType.ResourcesRequiredToBuild)
            {
                for (int i = 0; i < resource.Value; i++)
                {
                    _jobQueue.Enqueue(new CarryInJob
                    {
                        ResourceType = resource.Key,
                        Target = building
                    });
                }
            }
        }
    }
}
