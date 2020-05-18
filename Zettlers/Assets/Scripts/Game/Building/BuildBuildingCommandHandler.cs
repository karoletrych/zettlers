using System;
using System.Collections.Generic;

namespace zettlers
{
    class BuildBuildingCommandHandler : IPlayerCommandHandler<BuildBuildingCommand>
    {
        public BuildBuildingCommandHandler()
        {
        }

        public void Handle(BuildBuildingCommand command)
        {
            Building building = new Building
            {
                Type = command.BuildingType,
                Id = Guid.Empty,
                Position = command.Position
            };

            foreach (KeyValuePair<ResourceType, int> resource in 
                command.BuildingType.ResourcesRequired)
            {
                for (int i = 0; i < resource.Value; i++)
                {
                    // _jobQueue.Enqueue(new CarryInJob
                    // {
                    //     ResourceType = resource.Key,
                    //     Target = building
                    // });
                }
            }
        }
    }
}
